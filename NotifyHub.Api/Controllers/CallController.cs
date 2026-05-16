using Microsoft.AspNetCore.Mvc;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Application.Services;
using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Application.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NotifyHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallController : ControllerBase
    {
        private readonly CallService _service;
        private readonly CallWorkflowService _CallWorkflowService;

        public CallController(CallService service, CallWorkflowService callWorkflowService)
        {
            _service = service;
            _CallWorkflowService = callWorkflowService;
        }

        // ✅ Create Call
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCall([FromBody] CreateCallDTO call)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid currentUserId = Guid.Parse(userId);

            if (call == null)
                return BadRequest("Call data is required");

            var callreturn = await _service.CreateCallAsync(call, currentUserId);
            await _CallWorkflowService.CreateCallWithStackAsync(callreturn.CallID);

            return Ok("Call created successfully");
        }

        // ✅ Get All Calls
        [HttpGet]
        public async Task<IActionResult> GetAllCalls()
        {
            var calls = await _service.GetAllCalls();
            return Ok(calls);
        }

        // ✅ Get Call by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCallById(Guid id)
        {
            var call = await _service.GetCallByIdAsync(id);

            if (call == null)
                return NotFound();

            return Ok(call);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCall(Guid id, [FromBody] UpdateCallDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            await _service.UpdateCallByAsync(id, dto);
           
            return Ok("Call Updated Succesfully");
        }
    }
}