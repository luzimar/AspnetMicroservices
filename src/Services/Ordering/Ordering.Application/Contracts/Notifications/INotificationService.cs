using Ordering.Application.Models;
using System.Collections.Generic;

namespace Ordering.Application.Contracts.Notifications
{
    public interface INotificationService
    {
        bool HasNotications();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
