using api_mongodb.ChargeDatabase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_mongodb.ChargeDatabase
{
    public class ChargeDatabaseController : ControllerBase
    {
        private readonly IChargeDatabaseCore _chargeDatabaseCore;
        public ChargeDatabaseController(IChargeDatabaseCore chargeDatabaseCore)
        {

            _chargeDatabaseCore = chargeDatabaseCore;

        }

        [HttpGet]
        [Route("/charge")]
        public async Task<IActionResult> Charge()
        {
            var pokemons = await _chargeDatabaseCore.ChargePokemons();
            return Ok(pokemons);
        }
    }
}
