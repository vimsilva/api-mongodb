using api_mongodb.ChargeDatabase.Entities;
using api_mongodb.Core;
using api_mongodb.Infrastructure.Data.Interfaces;
using MongoDB.Driver;
using System.Xml.Linq;
using ZstdSharp.Unsafe;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace api_mongodb.Infrastructure.Data.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonContext _context;
        public PokemonRepository(IPokemonContext context)
        {
            _context = context;
        }
        public async Task<bool> ChargePokemons(IList<Pokemon> pokemons)
        {
            foreach (var poke in pokemons)
            {
                await _context.Pokemons.InsertOneAsync(poke);
            }
            return true;
        }

        public async Task<Pokemon> GetPokemonByName(string name)
        {
            return await _context
                .Pokemons
                .Find(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<Pokemon> GetPokemonByNumber(int number)
        {
            return await _context
                .Pokemons
                .Find(p => p.PokemonId == number)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _context
                .Pokemons
                .Find(p => true)
                .ToListAsync();
        }
    }
}
