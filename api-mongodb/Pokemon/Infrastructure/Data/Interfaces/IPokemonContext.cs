using api_mongodb.ChargeDatabase.Entities;
using MongoDB.Driver;

namespace api_mongodb.Infrastructure.Data.Interfaces
{
    public interface IPokemonContext
    {
        IMongoCollection<PokemonEntity> Pokemons { get; }
    }
}
