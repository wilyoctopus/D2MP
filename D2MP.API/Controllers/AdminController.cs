using D2MP.Models.Models;
using D2MP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace D2MP.API.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMatchScrapingService _matchScrapingService;
        private readonly IStatisticsService _statisticsService;

        public AdminController(IMatchScrapingService matchScrapingService,
                               IStatisticsService statisticsService)
        {
            _matchScrapingService = matchScrapingService;
            _statisticsService = statisticsService;
        }

        [HttpPut]
        [Route("scrape/start")]
        public ActionResult StartScrape(long matchSeqNum = -1)
        {
            _matchScrapingService.Start(matchSeqNum);

            return Ok();
        }

        [HttpPut]
        [Route("scrape/stop")]
        public ActionResult StopScrape()
        {
            _matchScrapingService.Stop();

            return Ok();
        }

        [HttpGet]
        [Route("scrape/status")]
        public ActionResult<ScrapingStatus> ScrapingStatus()
        {
            var status = _matchScrapingService.GetStatus();

            return Ok(status);
        }

        [HttpPut]
        [Route("statistics/recalculate")]
        public async Task<ActionResult> RecalculateStatistics()
        {
            await _statisticsService.CalculateHeroDuos();

            return Ok();
        }
    }
}