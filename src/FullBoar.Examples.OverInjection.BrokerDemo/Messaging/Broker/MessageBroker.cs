using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker
{
    public sealed class MessageBroker : IMessageBroker
    {
        #region Member Variables
        private int _disposed;
        #endregion

        #region Properties
        public bool IsDisposed => _disposed == 1;
        #endregion

        #region IMessageBroker Implementation
        public void Publish<T>(T message, Action<MessageBrokerError> onError = null)
        {
            var localSubscriptions = Subscriptions.GetTheLatestRevisionOfSubscriptions();

            var msgType = typeof(T);

            // ReSharper disable once ForCanBeConvertedToForeach | Performance Critical
            for (var idx = 0; idx < localSubscriptions.Length; idx++)
            {
                var subscription = localSubscriptions[idx];

                if (!subscription.Type.GetTypeInfo().IsAssignableFrom(msgType)) { continue; }

                try
                {
                    subscription.Handle(message);
                }
                catch(Exception e)
                {
                    Action<MessageBrokerError> copy = onError;

                    if(copy == null)
                        throw;

                    copy.Invoke(
                        new MessageBrokerError
                        {
                            Exception = e,
                            SubscriptionToken = subscription.Token
                        });
                }
            }
        }

        public void Publish(Object message, Type messageType, Action<MessageBrokerError> onError = null)
        {
            var localSubscriptions = Subscriptions.GetTheLatestRevisionOfSubscriptions();

            // ReSharper disable once ForCanBeConvertedToForeach | Performance Critical
            for (var idx = 0; idx < localSubscriptions.Length; idx++)
            {
                var subscription = localSubscriptions[idx];

                if (!subscription.Type.GetTypeInfo().IsAssignableFrom(messageType)) { continue; }

                try
                {
                    subscription.Handle(message);
                }
                catch(Exception e)
                {
                    Action<MessageBrokerError> copy = onError;

                    if(copy == null)
                        throw;

                    copy.Invoke(
                        new MessageBrokerError
                        {
                            Exception = e,
                            SubscriptionToken = subscription.Token
                        });
                }
            }
        }

        public Guid Subscribe<T>(Action<T> action)
        {
            return Subscribe(action, TimeSpan.Zero);
        }

        public Guid Subscribe<T>(Action<T> action, TimeSpan throttleBy)
        {
            EnsureNotNull(action);
            EnsureNotDisposed();

            return Subscriptions.Register(throttleBy, action);
        }

        public Guid Subscribe(Type type, Action<Object> action)
        {
            return Subscribe(type, action, TimeSpan.Zero);
        }

        public Guid Subscribe(Type type, Action<Object> action, TimeSpan throttleBy)
        {
            EnsureNotNull(action);
            EnsureNotDisposed();

            return Subscriptions.Register(type, throttleBy, action);
        }

        public void UnSubscribe(Guid token)
        {
            EnsureNotDisposed();
            Subscriptions.UnRegister(token);
        }

        public bool IsSubscribed(Guid token)
        {
            EnsureNotDisposed();
            return Subscriptions.IsRegistered(token);
        }
        #endregion

        #region Utility Methods
        public void Dispose()
        {
            Interlocked.Increment(ref _disposed);
            Subscriptions.Dispose();
        }

        [DebuggerStepThrough]
        private void EnsureNotDisposed()
        {
            if (_disposed == 1) { throw new ObjectDisposedException(GetType().Name); }
        }

        [DebuggerStepThrough]
        private void EnsureNotNull(object obj)
        {
            if (obj == null) { throw new NullReferenceException(nameof(obj)); }
        }
        #endregion
    }
}
