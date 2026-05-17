using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Hubs;
using NotifyHub.Api.Source.Application.Common.Models;
using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Application.Interface;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Infrastructure.Repositories;


namespace NotifyHub.Api.Source.Infrastructure.Services
{
    public class NotificationService: INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationRepository _repo;

        public NotificationService(IHubContext<NotificationHub> hubContext, INotificationRepository repo)
        {
            _hubContext = hubContext;
            _repo = repo;
        }
        public async Task SendNotification(string type, object data)
        {
            var message = new NotificationMessage
            {
                Type = type,
                Data = data
            };

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
        }
        public async Task CreateNotification(string message,CallResponseDTO CallResponse, Guid userId)
        {
            var notification = new Notification
            {
                NotifyId = Guid.NewGuid(),
                Message = message,
                UserId = userId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddAsync(notification);

            // SignalR push
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async Task<List<Notification>> GetUnread(Guid userId)
        {
            return await _repo.GetUnreadAsync(userId);
        }

        public async Task MarkAsRead(Guid notifyId)
        {
            await _repo.MarkAsReadAsync(notifyId);
        }
    }
}
