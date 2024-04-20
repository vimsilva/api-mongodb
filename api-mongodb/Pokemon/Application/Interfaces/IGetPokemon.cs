using api_mongodb.Core;

namespace api_mongodb.Application.Interfaces
{
    public interface IGetPokemon
    {
        Task<List<Pokemon>> GetPokemons();
        Task<Pokemon> GetPokemonByNumber(int id);
        Task<Pokemon> GetPokemonByName(string name);
    }
}
