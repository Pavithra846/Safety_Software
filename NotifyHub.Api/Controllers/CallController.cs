using Microsoft.AspNetCore.Mvc;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace NotifyHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallController : ControllerBase
    {
        private readonly CallService _service;

        public CallController(CallService service)
        {
            _service = service;
        }

        // ✅ Create Call
        [HttpPost]
        public async Task<IActionResult> CreateCall([FromBody] Call call)
        {
            if (call == null)
                return BadRequest("Call data is required");

            await _service.CreateCallAsync(call);

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
        public async Task<IActionResult> UpdateCall(Guid id, [FromBody] Call updatedCall)
        {
            if (id != updatedCall?.CallID)
            {
                return BadRequest("ID mismatch");
            }

            await _service.UpdateCallByAsync(updatedCall);
           
            return Ok("Call Updated Succesfully");
        }
    }
}