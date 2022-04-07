namespace D2MP.Models
{
    public class HeroDuo : IEquatable<HeroDuo>
    {
        public short Hero1Id { get; set; }
        public short Hero2Id { get; set; }

        public string Hero1Name { get; set; }
        public string Hero2Name { get; set; }

        public int MatchesWon = 0;
        public int MatchesLost = 0;

        public double MatchesCount => (MatchesWon + MatchesLost);
        public double WinRate => MatchesCount == 0 ? 0 : Math.Round(((double)MatchesWon / (double)(MatchesWon + MatchesLost)) * 100, 2);

        public override string ToString()
        {
            return $"{Hero1Name}, {Hero2Name}: WR% - {WinRate.ToString("N2")}; Match Count - {MatchesCount}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hero1Id, Hero2Id);
        }

        public override bool Equals(object obj)
        {
            var other = obj as HeroDuo;
            if (other == null) return false;

            return this.Equals(other);
        }

        public bool Equals(HeroDuo other)
        {
            return this.Hero1Id == other.Hero1Id && this.Hero2Id == other.Hero2Id;
        }
    }
}
