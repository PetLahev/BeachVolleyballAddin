using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeachVolleyballAddin.Data
{
    internal class TournamentInfo
    {
        internal int  Index { get; set; }
        internal string Name { get; set;}
        internal string Type { get; set; }
        internal string Country { get; set; }
        internal string MenLink { get; set; }
        internal string WomenLink { get; set; }
        internal bool HasMen
        {
            get { return !string.IsNullOrWhiteSpace(MenLink); }
        }
        internal bool HasWomen
        {
            get { return !string.IsNullOrWhiteSpace(WomenLink); }
        }

        internal List<RankingTable> StandingsMen { get; set; } = new List<RankingTable>();
        internal List<RankingTable> StandingsWomen { get; set; } = new List<RankingTable>();

        public override string ToString()
        {
            return $"{Name} ({Type}/{Country})";
        }

    }
}
