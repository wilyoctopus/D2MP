using D2MP.Models;

namespace D2MP.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<IOrderedEnumerable<HeroDuo>> CalculateHeroDuos();
        Task<IEnumerable<HeroDuo>> GetDuoStats(int page = 1, string heroName = null, int minMatchCount = -1);
        Task<long> GetMatchCount();
    }
}
