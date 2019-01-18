using System;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Events;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Services
{
    public class NotificationService : INotificationService
    {
        #region Member Variables
        private readonly Guid _overdraftSub;
        #endregion

        #region Constructor
        public NotificationService(IMessageBroker broker)
        {
            _overdraftSub = broker.Subscribe<AccountOverWithdrawn>(OnAccountOverWithdrawn);
        }
        #endregion

        #region INotificationService Implementation
        public void SendNotification(string notification)
        {
            // Do something interesting here
        }
        #endregion

        #region Event Handlers
        private void OnAccountOverWithdrawn(AccountOverWithdrawn evt)
        {
            SendNotification("Account over withdrawn");
            //_logger.Information("Account over withdrawn");
        }

        private void OnCheckFailedValidation(CheckFailedValidation evt)
        {
            //_logger.Information("Invalid check: {@Check}", evt.Check);
        }
        #endregion
    }
}
