using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace BeachVolleyballAddin.Internals
{
    internal class TournamnetsInfoManager
    {
        internal void CollectTournamentsData(Excel.Workbook workbook)
        {
            try
            {
                var sheets = workbook.Worksheets;
                Excel.Worksheet dataSheet = sheets.Add();

                var parser = new Tournaments.Parser();
                parser.GetTournamentsData();

                foreach (var tournament in parser.Tournaments)
                {
                    if (tournament.HasMen)
                    {
                        tournament.StandingsMen.AddRange(parser.GetTournamentRankingTable(tournament.MenLink));
                    }

                    if (tournament.HasWomen)
                    {
                        tournament.StandingsWomen.AddRange(parser.GetTournamentRankingTable(tournament.WomenLink));
                    }
                }

                Excel.Range cells = dataSheet.Cells;
                cells[1, 1].Value = "Tournament Name";
                cells[1, 2].Value = "Tournament Type";
                cells[1, 3].Value = "Tournament Country";
                cells[1, 4].Value = "Category";
                cells[1, 5].Value = "Team Standing";
                cells[1, 6].Value = "Team Name";
                cells[1, 7].Value = "Team Country";
                cells[1, 8].Value = "Points";
                cells[1, 9].Value = "Earnings $";

                var rowIndex = 2;
                foreach (var tournament in parser.Tournaments)
                {
                    foreach (var men in tournament.StandingsMen)
                    {
                        cells[rowIndex, 1].Value = tournament.Name;
                        cells[rowIndex, 2].Value = tournament.Type;
                        cells[rowIndex, 3].Value = tournament.Country;
                        cells[rowIndex, 4].Value = "M";
                        cells[rowIndex, 5].Value = men.Ranking;
                        cells[rowIndex, 6].Value = men.Team;
                        cells[rowIndex, 7].Value = men.Country;
                        cells[rowIndex, 8].Value = men.Points;
                        cells[rowIndex, 9].Value = men.Earnings;
                        rowIndex++;
                    }

                    foreach (var women in tournament.StandingsWomen)
                    {
                        cells[rowIndex, 1].Value = tournament.Name;
                        cells[rowIndex, 2].Value = tournament.Type;
                        cells[rowIndex, 3].Value = tournament.Country;
                        cells[rowIndex, 4].Value = "W";
                        cells[rowIndex, 5].Value = women.Ranking;
                        cells[rowIndex, 6].Value = women.Team;
                        cells[rowIndex, 7].Value = women.Country;
                        cells[rowIndex, 8].Value = women.Points;
                        cells[rowIndex, 9].Value = women.Earnings;
                        rowIndex++;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
