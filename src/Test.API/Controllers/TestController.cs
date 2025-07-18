using Microsoft.AspNetCore.Mvc;

namespace Test.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("ping")]
        public async Task<string> GetPong()
        {
            return "pong";
        }
    }
}
