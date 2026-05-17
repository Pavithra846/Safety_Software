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
        private readonly CallWorkflowService _CallWorkflowService;

        public StackController(StackService service, CallWorkflowService callWorkflowService)
        {
            _service = service;
            _CallWorkflowService = callWorkflowService;
        }

        // ✅ Create stack
        [HttpPost]
        public async Task<IActionResult> CreateStack([FromBody] CreateStackDto dto)
        {
            if (dto == null)
                return BadRequest("stack data is required");

            //await _service.CreateStackAsync(dto);

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
            bool hasStacks = false; Guid callid = Guid.Empty;
             (hasStacks, callid) = await _service.UpdateStackByAsync(id, dto);
            if(hasStacks) await _CallWorkflowService.FinishStackAndCallAsync(callid, true);

            return Ok("Stack Updated Successfully");
        }
    }
}
