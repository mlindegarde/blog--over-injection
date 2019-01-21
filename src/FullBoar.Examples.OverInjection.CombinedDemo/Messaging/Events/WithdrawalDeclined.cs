using FullBoar.Examples.OverInjection.CombinedDemo.Model;
using NodaTime;

namespace FullBoar.Examples.OverInjection.CombinedDemo.Messaging.Events
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
