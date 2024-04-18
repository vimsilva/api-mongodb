using api_mongodb.ChargeDatabase.Entities;

namespace api_mongodb.ChargeDatabase.Interfaces
{
    public interface IChargeDatabaseCore
    {
        public Task<string> ChargePokemons();
    }
}