using Microsoft.EntityFrameworkCore;
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

        public async Task AddAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Notification>> GetUnreadAsync(Guid userId)
        {
            return await _context.Notifications
                .Where(x => x.UserId == userId && !x.IsRead)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(Guid notifyId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(x => x.NotifyId == notifyId);

            if (notification != null)
            {
                notification.IsRead = true;

                await _context.SaveChangesAsync();
            }
        }
    }
}
