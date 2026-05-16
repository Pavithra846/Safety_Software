using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Source.Application.Interface
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<List<Notification>> GetUnreadAsync(Guid userId);

        Task MarkAsReadAsync(Guid notifyId);
    }
}
