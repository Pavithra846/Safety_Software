using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Source.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task CreateLoginDetails(Users dto);
        Task<Users> GetByUsernameAsync(string Username);
        Task UpdateAsync(Users dto);
    }
}
