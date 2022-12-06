
namespace BeachVolleyballAddin.Data
{
    public class RankingTable
    {        
        public string Ranking { get; set; }
        public string Team { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Country { get; set; }
        public string Points { get; set; }
        public string Earnings { get; set; }
       
        public override string ToString()
        {
            return $"{Team} ({Country})";
        }
    }
}
