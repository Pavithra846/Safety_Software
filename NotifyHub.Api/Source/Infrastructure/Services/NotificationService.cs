using Microsoft.AspNetCore.SignalR;
using NotifyHub.Api.Hubs;
using NotifyHub.Api.Source.Application.Common.Models;
using NotifyHub.Api.Source.Domain.Interfaces;


namespace NotifyHub.Api.Source.Infrastructure.Services
{
    public class NotificationService: INotificationService
    {
        private readonly IHubContext<CallHub> _hubContext;

        public NotificationService(IHubContext<CallHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendAsync(string type, object data)
        {
            var message = new NotificationMessage
            {
                Type = type,
                Data = data
            };

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
