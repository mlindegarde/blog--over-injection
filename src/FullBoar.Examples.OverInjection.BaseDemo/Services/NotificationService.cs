namespace FullBoar.Examples.OverInjection.BaseDemo.Services
{
    public class NotificationService : INotificationService
    {
        #region INotificationService Implementation
        public void SendNotification(string notification)
        {
            // Do something interesting here
        }
        #endregion
    }
}
