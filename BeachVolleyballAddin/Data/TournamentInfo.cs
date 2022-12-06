using System.Collections.Generic;
using System.Xml.Serialization;

namespace BeachVolleyballAddin.Data
{    
    public class TournamentInfo
    {
        [XmlElement]
        public string Version { get; set; }
        [XmlElement]
        public int  Index { get; set; }
        [XmlElement]
        public string Name { get; set;}
        [XmlElement]
        public string Type { get; set; }
        [XmlElement]
        public string Country { get; set; }
        [XmlElement]
        public string MenLink { get; set; }
        [XmlElement]
        public string WomenLink { get; set; }
        [XmlIgnore]
        public bool HasMen
        {
            get { return !string.IsNullOrWhiteSpace(MenLink); }
        }
        [XmlIgnore]
        public bool HasWomen
        {
            get { return !string.IsNullOrWhiteSpace(WomenLink); }
        }

        public List<RankingTable> StandingsMen { get; set; } = new List<RankingTable>();
        public List<RankingTable> StandingsWomen { get; set; } = new List<RankingTable>();

        public override string ToString()
        {
            return $"{Name} ({Type}/{Country})";
        }

    }
}
