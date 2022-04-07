using D2MP.Models.Models;

namespace D2MP.Services.Interfaces
{
    public interface IMatchScrapingService
    {
        void Start(long matchSeqNumber = -1);
        void Stop();
        ScrapingStatus GetStatus();
    }
}
