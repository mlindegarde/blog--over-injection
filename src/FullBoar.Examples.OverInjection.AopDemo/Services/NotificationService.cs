namespace FullBoar.Examples.OverInjection.AopDemo.Services
{
    public class NotificationService : INotificationService
    {
        #region INotificationService Implementation
        public void SendNotification(string notification)
        {
            // Do something interesting here.
        }
        #endregion
    }
}
