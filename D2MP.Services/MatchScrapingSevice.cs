using D2MP.Data.Interfaces;
using D2MP.Models;
using D2MP.Models.Enums;
using D2MP.Models.Models;
using D2MP.Services.Interfaces;
using D2MP.Services.Utils;
using System.Diagnostics;
using System.Text.Json;

namespace D2MP.Services
{
    internal class MatchScrapingSevice : IMatchScrapingService
    {
        private readonly IMatchService _matchService;
        private readonly IPartialMatchResultRepository _matchRepository;

        private static object _lock = new object();
        private static CancellationTokenSource _cts = null;
        private static Task _scrapeServiceTask = null;
        private static DateTime _lastProcessedMatchTime = DateTime.MinValue;

        public MatchScrapingSevice(IMatchService matchService,
                                   IPartialMatchResultRepository matchRepository)
        {
            _matchService = matchService;
            _matchRepository = matchRepository;
        }

        public void Start(long matchSeqNumber = -1)
        {
            if (_scrapeServiceTask != null && _cts != null) return;

            lock (_lock)
            {
                if (_scrapeServiceTask != null && _cts != null) return;

                if (matchSeqNumber == -1)
                    matchSeqNumber = _matchRepository.GetLastMatchSeqId().Result + 1;

                _cts = new CancellationTokenSource();
                _scrapeServiceTask = Task.Run(() => FetchData(matchSeqNumber), _cts.Token);
            }
        }

        public void Stop()
        {
            if (_scrapeServiceTask == null && _cts == null) return;

            lock (_lock)
            {
                if (_scrapeServiceTask == null && _cts == null) return;

                _cts?.Cancel();
                _cts = null;
                _scrapeServiceTask = null;
            }
        }

        public ScrapingStatus GetStatus()
        {
            return new ScrapingStatus()
            {
                LastProcessedMatchTime = _lastProcessedMatchTime,
                State = _scrapeServiceTask != null ? _scrapeServiceTask.Status.ToString() : "Not initialized"
            };
        }

        private async Task FetchData(long matchSeqNumber)
        {
            try
            {
                for (long i = matchSeqNumber; true;)
                {
                    if (_cts.Token.IsCancellationRequested)
                        return;

                    var result = await Retry.Do(() => _matchService.GetMatchHistoryBySequenceNumber(i.ToString()),
                                                   TimeSpan.FromSeconds(6),
                                                   _cts.Token);

                    if (result == null)
                        return;

                    await FilterAndSaveMatches(result.Matches);

                    i = result.Matches.OrderBy(x => x.MatchSequenceNumber)
                                      .Last()
                                      .MatchSequenceNumber + 1;

                    _lastProcessedMatchTime = DateTime.UtcNow;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(JsonSerializer.Serialize(ex));
            }
        }

        private async Task FilterAndSaveMatches(IEnumerable<DetailedMatch> matches)
        {
            matches = matches.Where(m => m.GameMode == GameMode.AllPick);

            foreach (var match in matches)
            {
                var team1heroIds = match.PickAndBans.Where(pb => pb.IsPick == true && pb.Faction == Faction.Radiant).Select(x => x.HeroId).ToArray();
                var team2heroIds = match.PickAndBans.Where(pb => pb.IsPick == true && pb.Faction == Faction.Dire).Select(x => x.HeroId).ToArray();

                // skip any non 5v5 matches
                if (team1heroIds.Count() + team2heroIds.Count() != 10)
                    continue;

                Array.Sort(team1heroIds);
                Array.Sort(team2heroIds);

                var team1won = match.WinningFaction == Faction.Radiant;
                var team2won = !team1won;

                var dbModel1 = new PartialMatchResult() { HeroIds = string.Join(',', team1heroIds), Won = team1won };
                var dbModel2 = new PartialMatchResult() { HeroIds = string.Join(',', team2heroIds), Won = team2won };

                await _matchRepository.Insert(dbModel1);
                await _matchRepository.Insert(dbModel2);
                await _matchRepository.Insert(match.MatchSequenceNumber);
            }
        }
    }
}
