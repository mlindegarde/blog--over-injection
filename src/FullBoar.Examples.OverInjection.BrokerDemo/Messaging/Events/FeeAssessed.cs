using FullBoar.Examples.OverInjection.BrokerDemo.Model;
using NodaTime;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Events
{
    public class FeeAssessed : ITransactionEvent<Fee>
    {
        #region Properties
        public Account Account { get; set; }
        public Fee Transaction { get; set; }
        public Instant CreatedAt { get; set; }
        #endregion
    }
}
