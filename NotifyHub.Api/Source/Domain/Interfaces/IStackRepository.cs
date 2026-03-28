using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Source.Domain.Interfaces
{
    public interface IStackRepository
    {
        Task<List<Stack>> GetAllAsync();
        Task<Stack> GetByIdAsync(Guid id);
        Task CreateAsync(Stack stack);
        Task SaveAsync();
        Task<int> GetMaxStackNumberForYearAsync(int year);
    }
}
