using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Events;
using FullBoar.Examples.OverInjection.BrokerDemo.Model;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Services
{
    public class BouncedCheckService : IBouncedCheckService, ISubscriber
    {
        #region Constants
        private const int BouncedCheckFee = 50;
        #endregion

        #region Member Variables
        private readonly IMessageBroker _broker;
        #endregion

        #region Constructor
        public BouncedCheckService(IMessageBroker broker)
        {
            _broker = broker;
        }
        #endregion

        #region ISubscriber Implementation
        public void RegisterSubscriptions()
        {
            _broker.Subscribe<CheckBounced>(OnCheckBounced);
        }
        #endregion

        #region Event Handlers
        public void OnCheckBounced(CheckBounced evt)
        {
            evt.Account.Process(new Fee(BouncedCheckFee));
        }
        #endregion
    }
}
