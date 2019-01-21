using FullBoar.Examples.OverInjection.AopDemo.Model;

namespace FullBoar.Examples.OverInjection.AopDemo.Services
{
    public interface IBouncedCheckService
    {
        void ApplyPenalty(Account account);
    }
}
