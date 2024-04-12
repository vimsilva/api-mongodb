using api_mongodb.ChargeDatabase.Entities;
using api_mongodb.ChargeDatabase.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace api_mongodb.ChargeDatabase
{
    public class ChargeDatabaseCore : IChargeDatabaseCore
    {
        public async Task<string> ChargePokemons()
        {
            var pokemons = await GetPokemons();
            foreach (var pokemon in pokemons)
            {
                var pokemonDetails = await GetPokemonDetais(pokemon.Url);
                pokemon.Copy(pokemonDetails);
                Console.WriteLine(pokemon);
            }
            return "Charge Database!";
        }

        private async Task<IList<Pokemon>> GetPokemons()
        {
            PokemonPayload pokemons = new PokemonPayload();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/");
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        pokemons = JsonSerializer.Deserialize<PokemonPayload>(content);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to get data. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            return Parse(pokemons);
        }

        private async Task<Pokemon> GetPokemonDetais(string url)
        {
            PokemonDetailsPayload pokemon = new PokemonDetailsPayload();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        pokemon = JsonSerializer.Deserialize<PokemonDetailsPayload>(content);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to get data. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            return Parse(pokemon);
        }

        private IList<Pokemon> Parse(PokemonPayload pokemonPayload)
        {
            IList<Pokemon> pokemons = new List<Pokemon>();
            foreach (var result in pokemonPayload.results)
            {
                pokemons.Add(new Pokemon
                {
                    Name = result.name,
                    Url = result.url
                });
            }
            return pokemons;
        }

        private Pokemon Parse(PokemonDetailsPayload pokemonDetailsPayload)
        {
            return new Pokemon
            {
                Id = pokemonDetailsPayload.id,
                Order = pokemonDetailsPayload.order,
                Name = pokemonDetailsPayload.name,
                Height = pokemonDetailsPayload.height,
                Weight = pokemonDetailsPayload.weight,
                Type = pokemonDetailsPayload.types.Select(t => t.type.name).ToList()
            };
        }
    }
}