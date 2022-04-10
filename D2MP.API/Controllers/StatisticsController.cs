using AutoMapper;
using D2MP.API.DTO;
using D2MP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace D2MP.API.Controllers
{
    [ApiController]
    [Route("statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IMapper _mapper;

        public StatisticsController(IStatisticsService statisticsService,
                                    IMapper mapper)
        {
            _statisticsService = statisticsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string? heroName, int? minMatchCount)
        {
            var result = await _statisticsService.GetDuoStats(heroName: heroName,
                                                              minMatchCount: minMatchCount.HasValue 
                                                                ? minMatchCount.Value 
                                                                : 0
                                                              );
            var response = _mapper.Map<IEnumerable<DuoStats>>(result);

            return Ok(response);
        }

        [HttpGet]
        [Route("matchCount")]
        public async Task<ActionResult> MatchCount()
        {
            var matchCount = await _statisticsService.GetMatchCount();
            return Ok(new { matchCount });
        }
    }
}