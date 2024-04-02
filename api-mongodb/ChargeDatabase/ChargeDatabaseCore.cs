using api_mongodb.ChargeDatabase.Entities;
using api_mongodb.ChargeDatabase.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace api_mongodb.ChargeDatabase
{
    public class ChargeDatabaseCore : IChargeDatabaseCore
    {
        public async Task<IList<Pokemon>> GetPokemons()
        {
            IList<Pokemon> pokemons = new List<Pokemon>();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/");
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        pokemons = JsonSerializer.Deserialize<IList<Pokemon>>(content);
                        Console.WriteLine(content);
                    }
                    else { 
                        Console.WriteLine($"Failed to get data. Status code: {response.StatusCode}");
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            return pokemons;
        }
    }
}
