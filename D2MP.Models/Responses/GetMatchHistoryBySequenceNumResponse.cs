using Newtonsoft.Json;

namespace D2MP.Models.Responses
{
    public class GetMatchHistoryBySequenceNumResponse
    {
        private int _status;
        private string _statusDetail;
        private List<DetailedMatch> _matches;

        [JsonProperty("matches")]
        public List<DetailedMatch> Matches
        {
            get { return _matches; }
            set { _matches = value; }
        }

        [JsonProperty("status")]
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [JsonProperty("statusDetail")]
        public string StatusDetail
        {
            get { return _statusDetail; }
            set { _statusDetail = value; }
        }
    }
}
