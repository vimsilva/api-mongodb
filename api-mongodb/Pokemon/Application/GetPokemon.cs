using api_mongodb.Application.Interfaces;
using api_mongodb.Core;
using api_mongodb.Infrastructure.Data.Interfaces;

namespace api_mongodb.Application
{
    public class GetPokemon : IGetPokemon
    {
        private readonly IPokemonRepository _pokemonRepository;

        public GetPokemon(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        public async Task<List<Pokemon>> GetPokemons()
        {
            var pokemons = await _pokemonRepository.GetPokemons();
            return pokemons;
        }

        public async Task<Pokemon> GetPokemonByNumber(int number)
        {
            var pokemon = await _pokemonRepository.GetPokemonByNumber(number);
            return pokemon;
        }

        public async Task<Pokemon> GetPokemonByName(string name)
        {
            var pokemon = await _pokemonRepository.GetPokemonByName(name);
            return pokemon;
        }

    }
}
