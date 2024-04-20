using api_mongodb.ChargeDatabase.Entities;
using api_mongodb.Core;
using MongoDB.Driver;

namespace api_mongodb.Infrastructure.Data.Interfaces
{
    public interface IPokemonContext
    {
        IMongoCollection<Pokemon> Pokemons { get; }
    }
}
