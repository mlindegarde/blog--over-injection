using System;

namespace FullBoar.Examples.OverInjection.Domain.Services
{
    public class NotificationService : INotificationService
    {
        #region INotificationService Implementation
        public void SendNotification(string notification)
        {
            Console.WriteLine(notification);
        }
        #endregion
    }
}
