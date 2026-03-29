using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Source.Domain.Interfaces
{
    public interface IUnitRepository
    {
        Task<List<Unit>> GetAllAsync();
        Task<Unit> GetByIdAsync(Guid id);
        Task CreateAsync(Unit unit);    
        Task UpdateAsync(Unit unit);
    }
}
