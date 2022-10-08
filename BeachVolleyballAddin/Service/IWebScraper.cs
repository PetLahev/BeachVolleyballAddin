
namespace BeachVolleyballAddin.Service
{
    interface IWebScraper
    {
        string GetAllTournaments(int year, string type = "all");

        string GetTournament(string url);
    }
}
