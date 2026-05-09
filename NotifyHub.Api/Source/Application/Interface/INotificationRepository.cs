using NotifyHub.Api.Source.Domain.Entities;

namespace NotifyHub.Api.Source.Application.Interface
{
    public interface INotificationRepository
    {
        Task Add(Notification notification);
    }
}
