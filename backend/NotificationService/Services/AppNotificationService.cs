using NotificationService.Model;
using NotificationService.Repository;
using System.Linq.Expressions;

namespace NotificationService.Services
{
    public class AppNotificationService : IAppNotificationService

    {
        private readonly IRepository<AppNotification> _repository;
        

        public AppNotificationService(IRepository<AppNotification> repository)
        {
            _repository = repository;
        }
        public async Task AddNotification(AppNotification notification)
        {
            await _repository.InsertOneAsync(notification);
        }

        public IEnumerable<AppNotification> GetForUser(string userId)
        {
            Expression<Func<AppNotification, bool>> filterExpression = document => document.UserId.Equals(userId);
            return _repository.FilterBy(filterExpression);
        }
    }
}
