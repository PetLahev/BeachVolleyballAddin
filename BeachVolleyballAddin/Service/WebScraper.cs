using System;
using System.Net;

namespace BeachVolleyballAddin.Service
{
    internal class WebScraper : IWebScraper
    {
        //season=2022&amp;international=fivb&amp;week=&amp;json=x%22
        private static string mainPage = @"https://fivb.12ndr.at/scripts/calendar.php?";

        public string GetAllTournaments(int year, string type = "all")
        {
            var client = new WebClient();
            return client.DownloadString(mainPage + $"season={year.ToString()}&international={type}");
        }

        public string GetTournament(string url)
        {
            //https://fivb.12ndr.at/tournament?tcode=MDOH2022&timezone=0
            var client = new WebClient();
            return client.DownloadString(url);
        }
    }
}
