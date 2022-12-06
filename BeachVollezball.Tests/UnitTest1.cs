using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeachVolleyballAddin;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BeachVolleyball.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var sut = new BeachVolleyballAddin.Tournaments.Parser();
            sut.GetTournamentsData();
            var allTournaments = sut.Tournaments;

            var rank = sut.GetTournamentRankingTable(sut.Tournaments[0].MenLink);
        }

        [TestMethod]
        public void XmlSer()
        {
            var t1 = new BeachVolleyballAddin.Data.TournamentInfo() {
                Country = "CZK", Name = "Test", Version = "!.0"};

            string xml = "";
            XmlSerializer x = new XmlSerializer(typeof(BeachVolleyballAddin.Data.TournamentInfo));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    x.Serialize(writer, t1);
                    xml = sww.ToString(); // Your XML
                }
            }

        }

    }
}
