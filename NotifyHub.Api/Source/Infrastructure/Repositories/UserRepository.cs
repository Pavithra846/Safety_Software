using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Application.Common.Security;
using NotifyHub.Api.Source.Infrastructure.Authentication.Services;

namespace NotifyHub.Api.Source.Infrastructure.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateLoginDetails(Users dto)
        {
            await _context.Users.AddAsync(dto);
            await _context.SaveChangesAsync();
        }


        public async Task<Users?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
