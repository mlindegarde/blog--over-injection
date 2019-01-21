using FullBoar.Examples.OverInjection.BrokerDemo.Model;
using NodaTime;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Events
{
    public class CheckBounced : ITransactionEvent<Check>
    {
        #region Propreties
        public Account Account { get; set; }
        public Check Transaction { get; set; }
        public Instant CreatedAt { get; set; }
        #endregion
    }
}
