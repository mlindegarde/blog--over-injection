using FullBoar.Examples.OverInjection.BrokerDemo.Model;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Services
{
    public interface IOverdraftService
    {
        void ApplyPenalty(Account account);
    }
}
