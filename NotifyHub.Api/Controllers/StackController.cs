using Microsoft.AspNetCore.Mvc;
using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Application.Services;
using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StackController : ControllerBase
    {
        private readonly StackService _service;

        public StackController(StackService service)
        {
            _service = service;
        }

        // ✅ Create stack
        [HttpPost]
        public async Task<IActionResult> CreateStack([FromBody] CreateStackDto dto)
        {
            if (dto == null)
                return BadRequest("stack data is required");

            await _service.CreateStackAsync(dto);

            return Ok("stack created successfully");
        }

        // ✅ Get All stacks
        [HttpGet]
        public async Task<IActionResult> GetAllStack()
        {
            var stacks = await _service.GetAllStackAsync();
            return Ok(stacks);
        }

        // ✅ Get stack by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStackById(Guid id)
        {
            var stack = await _service.GetStackByIdAsync(id);

            if (stack == null)
                return NotFound();

            return Ok(stack);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStack(Guid id, [FromBody] UpdateStackDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            await _service.UpdateStackByAsync(id, dto);

            return Ok("Stack Updated Succesfully");
        }
    }
}
