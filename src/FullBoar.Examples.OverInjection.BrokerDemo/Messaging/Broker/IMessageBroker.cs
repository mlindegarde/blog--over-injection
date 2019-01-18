using System;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker
{
    public interface IMessageBroker : IDisposable
    {
        #region Properties
        bool IsDisposed { get; }
        #endregion

        #region Methods
        void Publish<T>(T message, Action<MessageBrokerError> onError = null);
        void Publish(Object message, Type messageType, Action<MessageBrokerError> onError = null);
        Guid Subscribe<T>(Action<T> action);
        Guid Subscribe(Type type, Action<Object> action);
        void UnSubscribe(Guid token);
        bool IsSubscribed(Guid token);
        #endregion
    }
}
