using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker
{
    public class MessageBroker : IMessageBroker
    {
        #region Member Variables
        private static readonly List<Subscription> Subscriptions = new List<Subscription>();
        #endregion

        #region IMessageBroker Implementation
        public void Publish<TMessage>(TMessage message)
        {
            Subscriptions
                .Where(s => s.Type.GetTypeInfo().IsAssignableFrom(typeof(TMessage)))
                .ToList()
                .ForEach(s => s.Handle(message));
        }

        public Guid Subscribe<TMessage>(Action<TMessage> action)
        {
            Subscription subscription =
                new Subscription
                {
                    Type = typeof(TMessage),
                    Token = Guid.NewGuid(),
                    Handler = action
                };

            Subscriptions.Add(subscription);
            return subscription.Token;
        }
        #endregion
    }
}
