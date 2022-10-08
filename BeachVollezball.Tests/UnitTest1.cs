using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeachVolleyballAddin;

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
    }
}
