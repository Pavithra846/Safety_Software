using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Source.Domain.Interfaces
{
    public interface IStackRepository
    {
        Task<List<Stack>> GetAllAsync();
        Task<Stack> GetByStackIdAsync(Guid id);
        Task<bool> HasStacksByCallIdAsync(Guid id);
        Task CreateAsync(Stack stack);
        Task SaveAsync();
        Task UpdateAsync(Stack stack);
        Task<int> GetMaxStackNumberForYearAsync(int year);
        Task DeleteAsync(Stack stack);
    }
}
