using D2MP.Data.Interfaces;
using D2MP.Infrastructure;
using D2MP.Models;
using D2MP.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace D2MP.Services
{
    internal class StatisticsService : IStatisticsService
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IPartialMatchResultRepository _matchRepository;
        private readonly IMemoryCache _cache;

        public StatisticsService(IHeroRepository heroRepository,
                                 IPartialMatchResultRepository matchRepository,
                                 IMemoryCache cache)
        {
            _heroRepository = heroRepository;
            _matchRepository = matchRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<HeroDuo>> GetDuoStats(int page = 1, string heroName = null, int matchCountFilter = 200)
        {
            var res = _cache.Get<IOrderedEnumerable<HeroDuo>>(Constants.CacheHeroDuosKey).AsEnumerable();
            var pageSize = 123;

            if (res == null)
                res = await CalculateHeroDuos();

            if (!string.IsNullOrEmpty(heroName))
                res = res.Where(x => x.Hero1Name.ToLower().Contains(heroName.ToLower()) 
                                  || x.Hero2Name.ToLower().Contains(heroName.ToLower()));

            res = res.Where(x => x.MatchesCount > matchCountFilter);

            res = res.OrderByDescending(x => x.WinRate)
                     .Skip(pageSize * (page - 1))
                     .Take(pageSize);
                     

            return res;
        }

        public async Task<IOrderedEnumerable<HeroDuo>> CalculateHeroDuos()
        {
            var matches = await _matchRepository.GetAll();
            var combos = await GetHeroDuos();
            
            Parallel.ForEach(matches, match =>
            {
                ProcessMatchResult(match, combos);
            });

            var wrFirst = combos.ToList().OrderByDescending(c => c.WinRate);

            _cache.Set(Constants.CacheHeroDuosKey, wrFirst);

            return wrFirst;
        }

        private void ProcessMatchResult(PartialMatchResult match, HashSet<HeroDuo> combos)
        {
            var heroCombosInMatch = Combinations(match.GetHeroIdsArray(), 2).Select(ids =>
            {
                return new HeroDuo() { Hero1Id = ids[0], Hero2Id = ids[1] }; //
            });

            foreach (var heroCombo in heroCombosInMatch)
            {
                var res = combos.TryGetValue(heroCombo, out var combo);

                if (match.Won)
                    Interlocked.Increment(ref combo.MatchesWon);
                else
                    Interlocked.Increment(ref combo.MatchesLost);
            }
        }

        private async Task<HashSet<HeroDuo>> GetHeroDuos()
        {
            var heroes = await _heroRepository.GetAll();
            var heroIds = heroes.Select(h => h.Id).OrderBy(id => id).ToList();
            var combos = Combinations(heroIds.ToArray(), 2);

            var res = new List<HeroDuo>();

            foreach (var comb in combos)
            {
                var combo = new HeroDuo()
                {
                    Hero1Id = comb[0],
                    Hero2Id = comb[1],

                    Hero1Name = heroes.First(h => h.Id == comb[0]).LocalizedName,
                    Hero2Name = heroes.First(h => h.Id == comb[1]).LocalizedName
                };

                res.Add(combo);
            }

            return new HashSet<HeroDuo>(res);
        }

        private static IEnumerable<T[]> Combinations<T>(T[] values, int k)
        {
            if (k < 0 || values.Length < k)
                yield break; // invalid parameters, no combinations possible

            // generate the initial combination indices
            var combIndices = new int[k];
            for (var i = 0; i < k; i++)
            {
                combIndices[i] = i;
            }

            while (true)
            {
                // return next combination
                var combination = new T[k];
                for (var i = 0; i < k; i++)
                {
                    combination[i] = values[combIndices[i]];
                }
                yield return combination;

                // find first index to update
                var indexToUpdate = k - 1;
                while (indexToUpdate >= 0 && combIndices[indexToUpdate] >= values.Length - k + indexToUpdate)
                {
                    indexToUpdate--;
                }

                if (indexToUpdate < 0)
                    yield break; // done

                // update combination indices
                for (var combIndex = combIndices[indexToUpdate] + 1; indexToUpdate < k; indexToUpdate++, combIndex++)
                {
                    combIndices[indexToUpdate] = combIndex;
                }
            }
        }
    }
}
