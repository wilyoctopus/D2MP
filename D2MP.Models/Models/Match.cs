using D2MP.Models.Converters;
using D2MP.Models.Enums;
using Newtonsoft.Json;

namespace D2MP.Models
{
    public class Match
    {
        private long _matchId;
        private long _matchSequenceNumber;
        private DateTime _startTime;
        private LobbyType _lobbyType;
        private List<Player> _players;
        
        [JsonProperty("match_id")]
        public long MatchId
        {
            get { return _matchId; }
            set { _matchId = value; }
        }

        [JsonProperty("match_seq_num")]
        public long MatchSequenceNumber
        {
            get { return _matchSequenceNumber; }
            set { _matchSequenceNumber = value; }
        }

        [JsonProperty("start_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        [JsonProperty("lobby_type")]
        [JsonConverter(typeof(NumberToEnumConverter<LobbyType>))]
        public LobbyType LobbyType
        {
            get { return _lobbyType; }
            set { _lobbyType = value; }
        }

        [JsonProperty("players")]
        public List<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }
    }
}
