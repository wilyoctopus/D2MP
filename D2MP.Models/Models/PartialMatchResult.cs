namespace D2MP.Models
{
    public class PartialMatchResult
    {
        public string HeroIds;
        public bool Won;

        public short[] GetHeroIdsArray() => HeroIds.Split(",").Select(x => short.Parse(x)).ToArray();
    }
}
