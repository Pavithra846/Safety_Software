using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Application.Services;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Authentication.Services;

namespace NotifyHub.Api.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _service;

        public LoginController(LoginService service)
        {
            _service = service;
            
        }
         //✅ Create login
        [HttpPost("Createlogin")]
        public async Task<IActionResult> CreateLoginDetails([FromBody] UserDTO dto)
        {
            if (dto == null)
                return BadRequest("User data is required");

            await _service.CreateLoginDetailsAsync(dto);

            return Ok("User created successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO dto)
        {
            var token = await _service.LoginAsync(dto); // ✅ get token

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { token }); // ✅ return token
        }
        [Authorize]
        [HttpGet("secure")]
        public IActionResult GetSecureData()
        {
            return Ok("This is protected");
        }
        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("Only admin can access");
        }
    }
}
