using FullBoar.Examples.OverInjection.Reference.Model;

namespace FullBoar.Examples.OverInjection.Reference.Services
{
    public interface IBouncedCheckService
    {
        void ApplyPenalty(Account account);
    }
}
