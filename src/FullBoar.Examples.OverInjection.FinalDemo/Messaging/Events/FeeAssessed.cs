using FullBoar.Examples.OverInjection.FinalDemo.Model;
using NodaTime;

namespace FullBoar.Examples.OverInjection.FinalDemo.Messaging.Events
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
