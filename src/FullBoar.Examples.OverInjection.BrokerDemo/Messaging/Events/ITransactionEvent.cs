using FullBoar.Examples.OverInjection.BrokerDemo.Model;
using NodaTime;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Events
{
    public interface ITransactionEvent<TTransaction> where TTransaction : ITransaction
    {
        #region Properties
        Account Account { get; set; }
        TTransaction Transaction { get; set; }
        Instant CreatedAt { get; set; }
        #endregion
    }
}
