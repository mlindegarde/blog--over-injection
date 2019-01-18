using System;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Events;
using FullBoar.Examples.OverInjection.BrokerDemo.Model;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Services
{
    public class OverdraftService : IOverdraftService
    {
        #region Constants
        private const int OverdraftFee = 35;
        #endregion

        #region Member Variables
        private readonly Guid _overdraftSub;
        #endregion

        #region Constructor
        public OverdraftService(IMessageBroker broker)
        {
            _overdraftSub = broker.Subscribe<AccountOverWithdrawn>(OnAccountOverWithdrawn);
        }
        #endregion

        #region IOverdraftService Implementation
        public void ApplyPenalty(Account account)
        {
            account.Balance -= OverdraftFee;
        }
        #endregion

        #region Event Handlers
        private void OnAccountOverWithdrawn(AccountOverWithdrawn evt)
        {
            ApplyPenalty(evt.Account);
        }
        #endregion
    }
}
