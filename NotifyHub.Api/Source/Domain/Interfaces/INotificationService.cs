namespace NotifyHub.Api.Source.Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(string type, object data);
    }
}
