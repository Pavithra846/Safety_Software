using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Source.Application.Interface
{
    public interface INotificationService
    {
        Task CreateNotification(string message,CallResponseDTO CallResponse, Guid userId);
        Task SendNotification(string type, object data);
        Task<List<Notification>> GetUnread(Guid userId);
        Task MarkAsRead(Guid notifyId);
    }
}
