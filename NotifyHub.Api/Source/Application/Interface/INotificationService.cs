namespace NotifyHub.Api.Source.Application.Interface
{
    public interface INotificationService
    {
        Task CreateNotification(string message, Guid userId);
    }
}
