﻿using Microsoft.AspNetCore.Mvc;
using Test.Classes;
using Test.Classes.DTOs.Requests;
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

            if (size == -1.0)
                return NotFound();

            return Ok(size);
        }

        [HttpGet("distance")]
        public async Task<IActionResult> GetDistanceFromToId([FromQuery] GetDistanceDTO getDistanceDTO)
        {
            var result = await _fieldService.GetDistanceToTheCenterAsync(getDistanceDTO);

            if (result == -1.0)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("inside")]
        public async Task<IActionResult> GetInside([FromQuery] Point point)
        {
            var result = await _fieldService.GetFieldInsideAsync(point);

            if (result == null)
                return Ok(false);

            return Ok($"id: {result.Id}, name: {result.Name}");
        }
    }
}
