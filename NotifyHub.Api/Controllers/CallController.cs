using Microsoft.AspNetCore.Mvc;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Application.Services;
using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Application.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            if (call == null)
                return BadRequest("Call data is required");
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Invalid token");
            Guid currentUserId = Guid.Parse(userId);


            var createdCall = await _service.CreateCallAsync(call, currentUserId);
            await _CallWorkflowService.CreateCallWithStackAsync(createdCall.CallID);

            return Ok(new
            {
                Message = "Call created successfully",
                CallId = createdCall.CallID
            });
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCall(Guid id, [FromBody] UpdateCallDTO dto)
        {
            await _service.UpdateCallByAsync(id, dto);

            return Ok("Call Updated Successfully");
        }
    }
}