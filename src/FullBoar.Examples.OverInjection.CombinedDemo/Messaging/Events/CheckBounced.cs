using FullBoar.Examples.OverInjection.CombinedDemo.Model;
using NodaTime;

namespace FullBoar.Examples.OverInjection.CombinedDemo.Messaging.Events
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
