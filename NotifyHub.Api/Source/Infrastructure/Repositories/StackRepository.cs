using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Data;

namespace NotifyHub.Api.Source.Infrastructure.Repositories
{
    public class StackRepository : IStackRepository
    {
        private readonly AppDbContext _context;

        public StackRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Stack stack)
        {
            await _context.Stacks.AddAsync(stack);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Stack>> GetAllAsync()
        {
            return await _context.Stacks.ToListAsync();
        }
        public async Task<Stack> GetByIdAsync(Guid Id)
        {
            return await _context.Stacks.FindAsync(Id);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetMaxStackNumberForYearAsync(int year)
        {
            return await _context.Stacks
                .Where(s => s.StkNbr.ToString().StartsWith(year.ToString()))
                .MaxAsync(s => (int?)s.StkNbr) ?? 0;
        }
    }
}
