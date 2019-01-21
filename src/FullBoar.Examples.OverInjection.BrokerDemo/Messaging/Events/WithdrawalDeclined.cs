using FullBoar.Examples.OverInjection.BrokerDemo.Model;
using NodaTime;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Events
{
    public class WithdrawalDeclined : ITransactionEvent<Withdrawal>
    {
        #region Properties
        public Account Account { get; set; }
        public Withdrawal Transaction { get; set; }
        public Instant CreatedAt { get; set; }
        #endregion
    }
}
