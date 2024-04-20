using api_mongodb.ChargeDatabase.Entities;
using api_mongodb.ChargeDatabase.Interfaces;
using api_mongodb.Core;
using api_mongodb.Infrastructure.Data.Interfaces;
using api_mongodb.Infrastructure.Tools;
using System.Net.Http.Json;
using System.Text.Json;

namespace api_mongodb.ChargeDatabase
{
    public class ChargeDatabaseCore : IChargeDatabaseCore
    {
        private readonly IPokemonRepository _pokemonRespoitory;
        private readonly IConfiguration _config;

        public ChargeDatabaseCore(IConfiguration config, IPokemonRepository pokemonRepository) 
        { 
            _pokemonRespoitory = pokemonRepository;
            _config = config;
        }
        public async Task<string> ChargePokemons()
        {
            var pokemons = await GetPokemonsFromPokemonApi();
            foreach (var pokemon in pokemons)
            {
                var pokemonDetails = await GetPokemonDetailsFromPokemonApi(pokemon.Url);
                pokemon.Copy(pokemonDetails);
            }
            var result = await SavePokemons(pokemons);
            return $"Charge Database: {result}";
        }

        private async Task<IList<Pokemon>> GetPokemonsFromPokemonApi()
        {
            var pokemons = await CurlRequest<PokemonPayload>.Get(_config.GetValue<string>("pokemon-api:BaseUrl"));
            return Parse(pokemons);
        }

        private async Task<Pokemon> GetPokemonDetailsFromPokemonApi(string url)
        {
            var pokemon = await CurlRequest<PokemonDetailsPayload>.Get(url);
            return Parse(pokemon);
        }
        private async Task<bool> SavePokemons(IList<Pokemon> pokemons)
        {
            return await _pokemonRespoitory.ChargePokemons(pokemons);
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