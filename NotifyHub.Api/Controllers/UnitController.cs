using Microsoft.AspNetCore.Mvc;
using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Application.Services;
using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly UnitService _service;

        public UnitController(UnitService service)
        {
            _service = service;
        }

        // ✅ Create Unit
        [HttpPost]
        public async Task<IActionResult> CreateUnit([FromBody] CreateUnitDTO unit)
        {
            if (unit == null)
                return BadRequest("Unit data is required");

            await _service.CreateUnitAsync(unit);

            return Ok("Unit created successfully");
        }

        // ✅ Get All Calls
        [HttpGet]
        public async Task<IActionResult> GetAllUnits()
        {
            var units = await _service.GetAllUnitAsync();
            return Ok(units);
        }

        // ✅ Get Call by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCallById(Guid id)
        {
            var unit = await _service.GetUnitByIdAsync(id);

            if (unit == null)
                return NotFound();

            return Ok(unit);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(Guid id, [FromBody] UpdateUnitDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            await _service.UpdateUnitByAsync(id, dto);

            return Ok("Unit Updated Successfully");
        }    
    }
}
