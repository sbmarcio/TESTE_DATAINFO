using System.Net.Http.Json;
using System.Text.Json;

namespace Questao2.Services
{
    public class FootballService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://jsonmock.hackerrank.com/api/football_matches";

        public FootballService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<int> GetTotalGoalsByTeamAndYear(string team, int year)
        {
            int totalGoals = 0;

            // Soma gols como team1
            totalGoals += await GetGoals(team, year, "team1");

            // Soma gols como team2
            totalGoals += await GetGoals(team, year, "team2");

            return totalGoals;
        }
        private async Task<int> GetGoals(string team, int year, string teamPosition)
        {
            int totalGoals = 0;
            int pagina = 1;
            int totalPaginas;

            do
            {
                var url = $"{BaseUrl}?year={year}&{teamPosition}={Uri.EscapeDataString(team)}&page={pagina}";

                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);

                if (response == null || response.Data == null)
                    break;

                totalPaginas = response.Total_Pages;

                foreach (var match in response.Data)
                {
                    if (teamPosition == "team1")
                        totalGoals += int.Parse(match.Team1Goals);
                    else
                        totalGoals += int.Parse(match.Team2Goals);
                }

                pagina++;

            } while (pagina <= totalPaginas);

            return totalGoals;
        }

        private class ApiResponse
        {
            public int Page { get; set; }
            public int Total_Pages { get; set; }
            public List<MatchData>? Data { get; set; }
        }

        private class MatchData
        {
            public string Team1 { get; set; }
            public string Team2 { get; set; }
            public string Team1Goals { get; set; }
            public string Team2Goals { get; set; }
        }


    }
}