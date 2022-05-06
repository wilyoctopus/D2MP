using D2MP.Infrastructure.Interfaces;
using D2MP.Models;
using D2MP.Models.Exceptions;
using D2MP.Models.Responses;
using D2MP.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace D2MP.Services
{
    public class MatchService : IMatchService
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly string _steamApiKey;
        private readonly string _baseAddres = "https://api.steampowered.com/";
        private readonly HttpClient _client;
        private readonly ILogger<MatchService> _logger;

        private string KeyString
        {
            get { return "?key=" + _steamApiKey; }
        }
        
        public MatchService(IConfigurationProvider configurationProvider, 
                            ILogger<MatchService> logger)
        {
            _configurationProvider = configurationProvider;
            _steamApiKey = _configurationProvider.GetSteamApiSecret();
            _client = new HttpClient();
            _logger = logger;
        }
        
        public async Task<DetailedMatch> GetDetailedMatch(string matchId)
        {
            var queryString = QueryBuilder(_baseAddres, "IDOTA2Match_570/GetMatchDetails/V001/", KeyString, "&match_id=", matchId);
            var content = await GetStringAsync(queryString);
            var apiResult = JsonConvert.DeserializeObject<ApiResponse<DetailedMatch>>(content);

            return apiResult.Result;
        }

        public async Task<ApiResponse<GetMatchHistoryBySequenceNumResponse>> GetMatchHistoryBySequenceNumber(string startAtMatchSeqNumber = null)
        {
            _logger.LogInformation("Getting match data for " + startAtMatchSeqNumber);

            StringBuilder extra = new StringBuilder();

            if (!string.IsNullOrEmpty(startAtMatchSeqNumber))
                extra.AppendFormat("{0}{1}", "&start_at_match_seq_num=", startAtMatchSeqNumber);

            string queryString = QueryBuilder(_baseAddres, "IDOTA2Match_570/GetMatchHistoryBySequenceNum/v0001/", KeyString, extra.ToString());
            string content = await GetStringAsync(queryString);

            if (content == null) return null;

            var apiResult = JsonConvert.DeserializeObject<ApiResponse<GetMatchHistoryBySequenceNumResponse>>(content);

            return apiResult;   
        }
        
        private string QueryBuilder(params string[] urlParts)
        {
            if (urlParts == null || !urlParts.Any())
                return string.Empty;
            return urlParts.Aggregate((a, b) => a + b);
        }

        private async Task<string> GetStringAsync(string address)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address);
            var response = await _client.SendAsync(request);
            
            string content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                _logger.LogError("Received 403 from STEAM API. Response body: " + content);
                throw new InvalidApiKeyException("Api-Key most likely wrong");
            }
                
            else if (response.StatusCode == HttpStatusCode.ServiceUnavailable || response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                _logger.LogError($"Received {response.StatusCode} from STEAM API. Response body: " + content);
                throw new ServiceUnavailableException("Server is busy or api-call limit exceeded. Please wait 30 seconds and try again. Call only ~1 request/second");
            }
            
            return content;
        }
    }
}