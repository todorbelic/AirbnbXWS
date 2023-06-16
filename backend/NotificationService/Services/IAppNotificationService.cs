using NotificationService.Model;

namespace NotificationService.Services
{
    public interface IAppNotificationService
    {
        public IEnumerable<AppNotification> GetForUser(string userId);
        public Task AddNotification(AppNotification notification);
    }
}
