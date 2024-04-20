using api_mongodb.Application.Interfaces;
using api_mongodb.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api_mongodb.Controllers
{
    public class GetPokemonController : ControllerBase
    {
        private readonly IGetPokemon _getPokemon;

        public GetPokemonController(IGetPokemon getPokemon)
        {
            _getPokemon = getPokemon;
        }


        [HttpGet]
        [Route("/getpokemons")]
        [ProducesResponseType(typeof(List<Pokemon>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPokemons()
        {
            var query = await _getPokemon.GetPokemons();
            return Ok(query);
        }

        [HttpGet]
        [Route("[action]/{pokemonNumber}", Name = "GetPokemonByNumber")]
        [ProducesResponseType(typeof(Pokemon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPokemonByNumber(int pokemonNumber)
        {
            var query = await _getPokemon.GetPokemonByNumber(pokemonNumber);
            return Ok(query);
        }

        [HttpGet]
        [Route("[action]/{pokemonName}", Name = "GetPokemonByName")]
        [ProducesResponseType(typeof(Pokemon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPokemonByName(string pokemonName)
        {
            var query = await _getPokemon.GetPokemonByName(pokemonName);
            return Ok(query);
        }
    }
}
