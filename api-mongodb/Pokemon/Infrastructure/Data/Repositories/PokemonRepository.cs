using api_mongodb.ChargeDatabase.Entities;
using api_mongodb.Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace api_mongodb.Infrastructure.Data.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonContext _context;
        public PokemonRepository(IPokemonContext context)
        {
            _context = context;
        }
        public async Task<bool> ChargePokemons(IList<PokemonEntity> pokemons)
        {
            foreach (var poke in pokemons)
            {
                await _context.Pokemons.InsertOneAsync(poke);
            }
            return true;
        }

        public async Task<PokemonEntity> GetPokemon(string id)
        {
            return await _context
                .Pokemons
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
