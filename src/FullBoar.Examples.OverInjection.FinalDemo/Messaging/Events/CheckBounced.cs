using FullBoar.Examples.OverInjection.FinalDemo.Model;
using NodaTime;

namespace FullBoar.Examples.OverInjection.FinalDemo.Messaging.Events
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
