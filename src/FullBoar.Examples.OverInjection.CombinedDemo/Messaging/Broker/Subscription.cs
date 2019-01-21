using System;

namespace FullBoar.Examples.OverInjection.CombinedDemo.Messaging.Broker
{
    public class Subscription
    {
        #region Properties
        public Guid Token { get; set; }
        public Type Type { get; set; }
        public object Handler { get; set; }
        #endregion

        #region Utility Methods
        public void Handle<TMessage>(TMessage message)
        {
            (Handler as Action<TMessage>)?.Invoke(message);
        }
        #endregion
    }
}
