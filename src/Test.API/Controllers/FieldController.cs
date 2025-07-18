using Microsoft.AspNetCore.Mvc;
using Test.Core.Services;

namespace Test.API.Controllers
{
    [Route("api/fields/")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly FieldService _fieldService;

        public FieldController(FieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _fieldService.GetAllFieldsAsync();

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("size")]
        public async Task<IActionResult> GetSizeById([FromQuery] int id)
        {
            var size = await _fieldService.GetSizeWithIdAsync(id);

            if (size == 0.0)
                return NotFound();

            return Ok(size);
        }
    }
}
