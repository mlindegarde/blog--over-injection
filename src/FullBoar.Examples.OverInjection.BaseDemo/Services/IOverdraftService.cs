using FullBoar.Examples.OverInjection.BaseDemo.Model;

namespace FullBoar.Examples.OverInjection.BaseDemo.Services
{
    public interface IOverdraftService
    {
        void ApplyPenalty(Account account);
    }
}
