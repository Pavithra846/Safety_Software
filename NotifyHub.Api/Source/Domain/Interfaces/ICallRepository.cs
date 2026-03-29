namespace NotifyHub.Api.Source.Domain.Interfaces
{
    using NotifyHub.Api.Source.Application.DTOs;
    using NotifyHub.Api.Source.Domain.Entities;
    public interface ICallRepository
    {
        Task CreateCallAsync(Call call);
        Task UpdateAsync(Call call);
        Task<List<Call>> GetAllAsync();
        Task<Call> GetByIdAsync(Guid callId);
    }
}
