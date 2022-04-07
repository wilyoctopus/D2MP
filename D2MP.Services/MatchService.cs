using D2MP.Infrastructure.Interfaces;
using D2MP.Models;
using D2MP.Models.Exceptions;
using D2MP.Models.Responses;
using D2MP.Services.Interfaces;
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

        private string KeyString
        {
            get { return "?key=" + _steamApiKey; }
        }
        
        // TODO: Logging
        public MatchService(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _steamApiKey = _configurationProvider.GetSteamApiSecret();
            _client = new HttpClient();
        }
        
        public async Task<DetailedMatch> GetDetailedMatch(string matchId)
        {
            var queryString = QueryBuilder(_baseAddres, "IDOTA2Match_570/GetMatchDetails/V001/", KeyString, "&match_id=", matchId);
            var content = await GetStringAsync(queryString);
            var apiResult = JsonConvert.DeserializeObject<ApiResponse<DetailedMatch>>(content);

            return apiResult.Result;
        }

        public async Task<GetMatchHistoryBySequenceNumResponse> GetMatchHistoryBySequenceNumber(string startAtMatchSeqNumber = null)
        {
            StringBuilder extra = new StringBuilder();

            if (!string.IsNullOrEmpty(startAtMatchSeqNumber))
                extra.AppendFormat("{0}{1}", "&start_at_match_seq_num=", startAtMatchSeqNumber);

            string queryString = QueryBuilder(_baseAddres, "IDOTA2Match_570/GetMatchHistoryBySequenceNum/v0001/", KeyString, extra.ToString());
            string content = await GetStringAsync(queryString);

            var apiResult = JsonConvert.DeserializeObject<ApiResponse<GetMatchHistoryBySequenceNumResponse>>(content);

            if (apiResult?.Result?.Status != 1)
                throw new ServiceUnavailableException("Something went wrong with the request, the status was != 1");

            return apiResult.Result;   
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

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new InvalidApiKeyException("Api-Key most likely wrong");
            else if (response.StatusCode == HttpStatusCode.TooManyRequests)
                throw new ServiceUnavailableException("Server is busy or api-call limit exceeded. Please wait 30 seconds and try again. Call only ~1 request/second.");
            else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                throw new ServiceUnavailableException("Server is busy or api-call limit exceeded. Please wait 30 seconds and try again. Call only ~1 request/second.");

            string content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}