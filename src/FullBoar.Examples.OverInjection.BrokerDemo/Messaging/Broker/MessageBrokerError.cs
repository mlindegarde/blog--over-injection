using System;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker
{
    public class MessageBrokerError
    {
        #region Properties
        public Exception Exception { get; set; }
        public Guid SubscriptionToken { get; set; }
        #endregion
    }
}
