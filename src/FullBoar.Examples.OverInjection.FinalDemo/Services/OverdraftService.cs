using FullBoar.Examples.OverInjection.FinalDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.FinalDemo.Messaging.Events;
using FullBoar.Examples.OverInjection.FinalDemo.Model;

namespace FullBoar.Examples.OverInjection.FinalDemo.Services
{
    public class OverdraftService : IOverdraftService, ISubscriber
    {
        #region Constants
        private const int OverdraftFee = 25;
        #endregion

        #region Member Variables
        private readonly IMessageBroker _broker;
        #endregion

        #region Constructor
        public OverdraftService(IMessageBroker broker)
        {
            _broker = broker;
        }
        #endregion

        #region ISubscriber Implementation
        public void Subscribe()
        {
            _broker.Subscribe<AccountOverWithdrawn>(OnAccountOverWithdrawn);
        }
        #endregion

        #region Event Handlers
        public void OnAccountOverWithdrawn(AccountOverWithdrawn evt)
        {
            evt.Account.Process(new Fee(OverdraftFee));
        }
        #endregion
    }
}
