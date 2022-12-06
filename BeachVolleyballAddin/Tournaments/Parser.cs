using System;
using BeachVolleyballAddin.Service;
using BeachVolleyballAddin.Data;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
using BeachVolleyballAddin.Internals;

namespace BeachVolleyballAddin.Tournaments
{
    internal class Parser
    {
        private IWebScraper serviceMngr = null;
        internal IWebScraper ServiceMngr
        {
            get
            {
                if (serviceMngr == null) serviceMngr = new WebScraper();
                return serviceMngr;
            }
        }

        internal List<TournamentInfo> Tournaments = new List<TournamentInfo>();

        internal void GetTournamentsData(string tournamentCategory = "fivb")
        {
            HtmlDocument doc = null;
            try
            {
                var tbl = ServiceMngr.GetAllTournaments(2022, tournamentCategory);
                doc = new HtmlDocument();
                doc.LoadHtml(tbl);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not tournaments data!" + ex.Message);
            }

            try
            {
                var rows = GetTableRows(GetTableBody(doc));
                int index = 0;
                foreach (var row in rows)
                {
                    index++;
                    var info = GetRowData(row);
                    Tournaments.Add(new TournamentInfo()
                    {
                        Index = index,
                        Name = info[0].InnerText,
                        MenLink = !string.IsNullOrWhiteSpace(info[1].InnerText) ? info[1].SelectSingleNode("a[@href]").Attributes[0].Value : "",
                        WomenLink = !string.IsNullOrWhiteSpace(info[2].InnerText) ? info[2].SelectSingleNode("a[@href]").Attributes[0].Value : "",
                        Type = info[3].InnerText,
                        Country = info[4].InnerText
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Parsing error!" + ex.Message);
            }
        }

        internal HtmlNodeCollection GetTournamentData(string url)
        {
            HtmlDocument doc = null;
            try
            {
                var html = ServiceMngr.GetTournament(url);
                doc = new HtmlDocument();
                doc.LoadHtml(html);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not download tournamnet info!" + ex.Message);
            }

            return doc.DocumentNode.SelectNodes("//table");
        }

        internal List<RankingTable> GetTournamentRankingTable(string url)
        {
            List<RankingTable> data = GetSavedRankingTable(url);
            if (data != null) return data; 
            try
            {
                data = new List<RankingTable>();
                var rankingTable = GetTournamentData(url).SingleOrDefault(x => x.Id.ToLower().Equals("ranking_table"));
                if (rankingTable == null) return data;

                var rows = GetTableRows(GetTableBody(rankingTable));
                var lastRanking = "";
                foreach (var row in rows)
                {
                    var info = GetRowData(row);
                    var ranking = info[0].InnerText;
                    if (string.IsNullOrWhiteSpace(ranking))
                    {
                        ranking = lastRanking;
                    }
                    else
                    {
                        lastRanking = ranking;
                    }
                    var tmp = new RankingTable()
                    {
                        Ranking = ranking,
                        Team = info[1].InnerText,
                        Country = info[2].InnerText,
                        Points = info[4].InnerText,
                        Earnings = info[5].InnerText
                    };
                    data.Add(tmp);
                }
                new RankingTablesLoader().SaveRankingTable(url, data);
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }

        internal List<RankingTable> GetSavedRankingTable(string url)
        {
            return new RankingTablesLoader().LoadRankingTable(url);
        }

        private HtmlNode GetTableBody(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//tbody");
        }

        private HtmlNode GetTableBody(HtmlNode table)
        {
            return table.SelectSingleNode("tbody");
        }

        private HtmlNode[] GetTableRows(HtmlNode tableBody)
        {
            return tableBody.SelectNodes("tr").ToArray();
        }

        private HtmlNode[] GetRowData(HtmlNode row)
        {
            return row.SelectNodes("td").ToArray();
        }
    }
}
