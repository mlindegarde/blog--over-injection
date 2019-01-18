using FullBoar.Examples.OverInjection.AopDemo.Model;

namespace FullBoar.Examples.OverInjection.AopDemo.Services
{
    public interface IOverdraftService
    {
        void ApplyPenalty(Account account);
    }
}
