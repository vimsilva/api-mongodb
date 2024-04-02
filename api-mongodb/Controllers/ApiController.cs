using Microsoft.AspNetCore.Mvc;

namespace api_mongodb.Controllers
{
    public class ApiController : ControllerBase
    {
        [HttpGet]
        [Route("/hello")]
        public IActionResult Hello()
        {
            return Ok("Hello API World!");
        }
    }
}
