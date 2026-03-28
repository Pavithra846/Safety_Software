using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;

namespace NotifyHub.Api.Source.Application.Services
{
    public class CallService
    {
        private readonly ICallRepository _repo;
        private readonly INotificationService _notification;

        public CallService(ICallRepository repo, INotificationService hub)
        {
            _repo = repo;
            _notification = hub;
        }

        public async Task CreateCallAsync(Call call)
        {
            // ✅ Business logic here
            if (call.CallID == Guid.Empty)
                call.CallID = Guid.NewGuid();

            if (call.CreatedDttm == DateTime.MinValue)
                call.CreatedDttm = DateTime.UtcNow;

            call.UpdatedDttm = DateTime.UtcNow;

            // Save to DB
            await _repo.CreateCallAsync(call);
            await _notification.SendAsync("CallCreated", call);
        }
        public async Task<List<Call>> GetAllCalls()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Call> GetCallByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task UpdateCallByAsync(Call call)
        {
            await _repo.UpdateAsync(call);
        }
    }
}
