using FullBoar.Examples.OverInjection.FinalDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.FinalDemo.Messaging.Events;
using Serilog;

namespace FullBoar.Examples.OverInjection.FinalDemo.Services
{
    public class NotificationService : INotificationService, ISubscriber
    {
        #region Member Variables
        private readonly ILogger _logger;
        private readonly IMessageBroker _broker;
        #endregion

        #region Constructor
        public NotificationService(IMessageBroker broker, ILogger logger)
        {
            _broker = broker;
            _logger = logger.ForContext<NotificationService>();
        }
        #endregion

        #region INotificationService Implementation
        public void SendNotification(string notification)
        {
            _logger.Information($"{nameof(NotificationService)} - {notification}", nameof(NotificationService));
        }
        #endregion

        #region ISubscriber Implementation
        public void RegisterSubscriptions()
        {
            _broker.Subscribe<WithdrawalDeclined>(OnWithdrawalDeclined);
            _broker.Subscribe<CheckBounced>(OnCheckBounced);
            _broker.Subscribe<AccountOverWithdrawn>(OnAccountOverWithdrawn);
            _broker.Subscribe<FeeAssessed>(OnFeeAssessed);
        }
        #endregion

        #region Event Handlers
        public void OnWithdrawalDeclined(WithdrawalDeclined evt)
        {
            SendNotification("You cannot withdraw more than is in the account");
        }

        public void OnCheckBounced(CheckBounced evt)
        {
            SendNotification("Check bounced, insufficient founds an overdrafts are not allowed");
        }

        public void OnAccountOverWithdrawn(AccountOverWithdrawn evt)
        {
            SendNotification("Account over withdrawn");
        }

        public void OnFeeAssessed(FeeAssessed evt)
        {
            SendNotification($"Fee assessed: {evt.Transaction.Amount}");
        }
        #endregion
    }
}
