using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeachVolleyballAddin.Data
{
    internal class RankingTable
    {        
        internal string Ranking { get; set; }
        internal string Team { get; set; }
        internal string Player1 { get; set; }
        internal string Player2 { get; set; }
        internal string Country { get; set; }
        internal string Points { get; set; }
        internal string Earnings { get; set; }
       
        public override string ToString()
        {
            return $"{Team} ({Country})";
        }
    }
}
