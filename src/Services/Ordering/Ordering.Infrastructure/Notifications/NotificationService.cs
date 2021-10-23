using Ordering.Application.Contracts.Notifications;
using Ordering.Application.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Infrastructure.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly List<Notification> _notifications;

        public NotificationService()
        {
            _notifications = new List<Notification>();
        }

        public List<Notification> GetNotifications() => _notifications;

        public void Handle(Notification notification) => _notifications.Add(notification);

        public bool HasNotications() => _notifications.Any();
    }
}
