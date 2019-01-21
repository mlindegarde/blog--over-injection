using System;

namespace FullBoar.Examples.OverInjection.CombinedDemo.Messaging.Broker
{
    public interface IMessageBroker
    {
        #region Methods
        void Publish<TMessage>(TMessage message);
        Guid Subscribe<TMessage>(Action<TMessage> action);
        #endregion
    }
}
