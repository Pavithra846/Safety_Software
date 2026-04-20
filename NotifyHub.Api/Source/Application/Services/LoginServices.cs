using NotifyHub.Api.Source.Application.Common.Security;
using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Authentication.Services;
using NotifyHub.Api.Source.Infrastructure.Repositories;

namespace NotifyHub.Api.Source.Application.Services
{
    public class LoginService
    {
        private readonly IUserRepository _repo;
        private readonly IJwtService _jwtService;
        public LoginService(IUserRepository repo, IJwtService jwtService)
        {
            _repo = repo;
            _jwtService = jwtService;
        }
        public async Task CreateLoginDetailsAsync(UserDTO dto)
        {
            var users = new Users
            {
                UserId = Guid.NewGuid(),
                Username = dto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };
            await _repo.CreateLoginDetails(users);
            var UserResponse = new UserResponseDTO
            {
                UserId = Guid.NewGuid(),
                Username = users.Username,
                Password = users.Password,
                Role = users.Role
            };
            

        }
        public async Task<string?> LoginAsync(UserDTO dto)
        {
            var user = await _repo.GetByUsernameAsync(dto.Username);

            if (user == null)
                return null;

            // 🔄 Handle old plain passwords
            if (!user.Password.StartsWith("$2"))
            {
                if (dto.Password != user.Password)
                    return null;

                user.Password = PasswordHelper.HashPassword(dto.Password);
                await _repo.UpdateAsync(user);
            }
            else
            {
                if (!PasswordHelper.VerifyPassword(dto.Password, user.Password))
                    return null;
            }

            // ✅ Generate JWT token
            var token = _jwtService.GenerateToken(user);

            return token;
        }
    }
}
