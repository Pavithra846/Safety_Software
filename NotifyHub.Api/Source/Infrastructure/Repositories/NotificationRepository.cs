using NotifyHub.Api.Source.Application.Interface;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Infrastructure.Data;

namespace NotifyHub.Api.Source.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}
