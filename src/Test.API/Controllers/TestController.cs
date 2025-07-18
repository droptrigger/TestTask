using Microsoft.AspNetCore.Mvc;

namespace Test.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TestController
    {
        [HttpGet("ping")]
        public async Task<string> GetPong()
        {
            return "pong";
        }
    }
}
