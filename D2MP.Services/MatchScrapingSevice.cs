using D2MP.Data.Interfaces;
using D2MP.Models;
using D2MP.Models.Enums;
using D2MP.Models.Models;
using D2MP.Services.Interfaces;
using D2MP.Services.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace D2MP.Services
{
    internal class MatchScrapingSevice : IMatchScrapingService
    {
        private readonly IMatchService _matchService;
        private readonly IPartialMatchResultRepository _matchRepository;
        private readonly ILogger<MatchScrapingSevice> _logger;

        private static object _lock = new object();
        private static CancellationTokenSource _cts = null;
        private static Task _scrapeServiceTask = null;
        private static DateTime _lastProcessedMatchTime = DateTime.MinValue;

        public MatchScrapingSevice(IMatchService matchService,
                                   IPartialMatchResultRepository matchRepository,
                                   ILogger<MatchScrapingSevice> logger)
        {
            _matchService = matchService;
            _matchRepository = matchRepository;
            _logger = logger;
        }

        public void Start(long matchSeqNumber = -1)
        {
            if (_scrapeServiceTask != null && _cts != null) return;

            lock (_lock)
            {
                if (_scrapeServiceTask != null && _cts != null) return;

                if (matchSeqNumber == -1)
                    matchSeqNumber = _matchRepository.GetLastMatchSeqId() + 1;

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
            _logger.LogInformation("Started fetching match data");

            try
            {
                for (long i = matchSeqNumber; true;)
                {
                    if (_cts.Token.IsCancellationRequested)
                        return;

                    var apiResponse = await Retry.Do(() => _matchService.GetMatchHistoryBySequenceNumber(i.ToString()),
                                                           TimeSpan.FromSeconds(6),
                                                           _cts.Token);

                    if (apiResponse == null)
                        return;

                    // Some match ids return "Error retrieving match data", have to skip them
                    if (apiResponse.Result.Status == 2)
                    {
                        _logger.LogWarning($"Received: {apiResponse.Result.StatusDetail}, skipping this match id");
                        i += 1;
                        continue;
                    }

                    if (apiResponse.Result.Status != 1)
                    {
                        throw new ApplicationException($"Unexcected status type: {apiResponse.Result.Status}. Status message: {apiResponse.Result.Status}. Json: {JsonConvert.SerializeObject(apiResponse)}");
                    }

                    await FilterAndSaveMatches(apiResponse.Result.Matches);

                    i = apiResponse.Result.Matches.OrderBy(x => x.MatchSequenceNumber)
                                                  .Last()
                                                  .MatchSequenceNumber + 1;

                    _lastProcessedMatchTime = DateTime.UtcNow;
                }
            }
            catch (AggregateException ae)
            {
                foreach (Exception ie in ae.Flatten().InnerExceptions)
                {
                    Exception nestedException = ie;
                    do
                    {
                        if (!string.IsNullOrEmpty(nestedException.Message))
                        {
                            _logger.LogError(nestedException.Message + "; StackTrace: " + nestedException.StackTrace);
                        }
                        nestedException = nestedException.InnerException;
                    }
                    while (nestedException != null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "; StackTrace: " + ex.StackTrace);
            }
        }

        private async Task FilterAndSaveMatches(IEnumerable<DetailedMatch> matches)
        {
            matches = matches.Where(m => m.GameMode == GameMode.AllPick && m.LobbyType == LobbyType.Ranked);

            foreach (var match in matches)
            {
                var radiantHeroIds = match.PickAndBans.Where(pb => pb.IsPick == true && pb.Faction == Faction.Radiant).Select(x => x.HeroId).ToArray();
                var direHeroIds = match.PickAndBans.Where(pb => pb.IsPick == true && pb.Faction == Faction.Dire).Select(x => x.HeroId).ToArray();

                // For some reason, ranked mode games sometimes have more than 10 heroes
                // Assuming that those are some custom lobbies/cheating/etc, we skip them
                if (radiantHeroIds.Count() + direHeroIds.Count() != 10)
                    continue;

                Array.Sort(radiantHeroIds);
                Array.Sort(direHeroIds);

                var pmr = new PartialMatchResult()
                {
                    RadiantWon = match.WinningFaction == Faction.Radiant,
                    RadiantHeroIds = string.Join(',', radiantHeroIds),
                    DireHeroIds = string.Join(',', direHeroIds),
                    MatchSeqNumber = match.MatchSequenceNumber
                };

                await _matchRepository.InsertAsync(pmr);
            }
        }
    }
}
