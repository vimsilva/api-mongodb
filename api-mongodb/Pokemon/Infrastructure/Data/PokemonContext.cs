using api_mongodb.ChargeDatabase.Entities;
using api_mongodb.Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace api_mongodb.Infrastructure.Data
{
    public class PokemonContext : IPokemonContext
    {
        public IMongoCollection<PokemonEntity> Pokemons { get; }

        public PokemonContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));

            Pokemons = database.GetCollection<PokemonEntity>(config.GetValue<string>("DatabaseSettings:PokemonCollection"));
        }
    }
}
