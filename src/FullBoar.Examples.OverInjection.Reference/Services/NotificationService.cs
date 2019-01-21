using Serilog;

namespace FullBoar.Examples.OverInjection.Reference.Services
{
    public class NotificationService : INotificationService
    {
        #region Member Variables
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public NotificationService(ILogger logger)
        {
            _logger = logger.ForContext<NotificationService>();
        }
        #endregion

        #region INotificationService Implementation
        public void SendNotification(string notification)
        {
            _logger.Information($"{nameof(NotificationService)} - {notification}", nameof(NotificationService));
        }
        #endregion
    }
}
