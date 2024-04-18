using api_mongodb.ChargeDatabase.Entities;
using api_mongodb.ChargeDatabase.Interfaces;
using api_mongodb.Infrastructure.Data.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace api_mongodb.ChargeDatabase
{
    public class ChargeDatabaseCore : IChargeDatabaseCore
    {
        private readonly IPokemonRepository _pokemonRespoitory;

        public ChargeDatabaseCore(IPokemonRepository pokemonRepository) 
        { 
            _pokemonRespoitory = pokemonRepository;
        }
        public async Task<string> ChargePokemons()
        {
            var pokemons = await GetPokemons();
            foreach (var pokemon in pokemons)
            {
                var pokemonDetails = await GetPokemonDetails(pokemon.Url);
                pokemon.Copy(pokemonDetails);
            }
            var result = await SavePokemons(pokemons);
            return $"Charge Database: {result}";
        }

        private async Task<IList<PokemonEntity>> GetPokemons()
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

        private async Task<PokemonEntity> GetPokemonDetails(string url)
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

        private IList<PokemonEntity> Parse(PokemonPayload pokemonPayload)
        {
            IList<PokemonEntity> pokemons = new List<PokemonEntity>();
            foreach (var result in pokemonPayload.results)
            {
                pokemons.Add(new PokemonEntity
                {
                    Name = result.name,
                    Url = result.url
                });
            }
            return pokemons;
        }

        private async Task<bool> SavePokemons(IList<PokemonEntity> pokemons)
        {
            return await _pokemonRespoitory.ChargePokemons(pokemons);
        }

        private PokemonEntity Parse(PokemonDetailsPayload pokemonDetailsPayload)
        {
            return new PokemonEntity
            {
                PokemonId = pokemonDetailsPayload.id,
                Order = pokemonDetailsPayload.order,
                Name = pokemonDetailsPayload.name,
                Height = pokemonDetailsPayload.height,
                Weight = pokemonDetailsPayload.weight,
                Type = pokemonDetailsPayload.types.Select(t => t.type.name).ToList()
            };
        }
    }
}