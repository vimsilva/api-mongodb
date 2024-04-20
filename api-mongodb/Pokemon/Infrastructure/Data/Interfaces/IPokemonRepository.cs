using api_mongodb.ChargeDatabase.Entities;
using api_mongodb.Core;

namespace api_mongodb.Infrastructure.Data.Interfaces
{
    public interface IPokemonRepository
    {
        Task<bool> ChargePokemons(IList<Pokemon> pokemon);
        Task<List<Pokemon>> GetPokemons();
        Task<Pokemon> GetPokemonByNumber(int id);
        Task<Pokemon> GetPokemonByName(string name);
    }
}
