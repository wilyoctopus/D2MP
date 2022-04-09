namespace D2MP.Models
{
    public class PartialMatchResult
    {
        public string RadiantHeroIds;
        public string DireHeroIds;
        public long MatchSeqNumber;
        public bool RadiantWon;

        public short[] GetRadiantHeroIdsArray() => RadiantHeroIds.Split(",").Select(x => short.Parse(x)).ToArray();
        public short[] GetDireHeroIdsArray() => DireHeroIds.Split(",").Select(x => short.Parse(x)).ToArray();
    }
}
