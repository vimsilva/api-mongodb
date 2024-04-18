using api_mongodb.ChargeDatabase.Entities;

namespace api_mongodb.Infrastructure.Data.Interfaces
{
    public interface IPokemonRepository
    {
        Task<bool> ChargePokemons(IList<PokemonEntity> pokemon);
        Task<PokemonEntity> GetPokemon(string id);
    }
}
