using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Data;

namespace NotifyHub.Api.Source.Infrastructure.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly AppDbContext _context;

        public UnitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Unit unit)
        {
            await _context.Units.AddAsync(unit);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Unit>> GetAllAsync()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<Unit> GetByIdAsync(Guid id)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                throw new Exception("Unit not found");
            }

            return unit;
        }

        public async Task UpdateAsync(Unit unit)
        {
            _context.Units.Update(unit);
            await _context.SaveChangesAsync();
        }
    }
}
