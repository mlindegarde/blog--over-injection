using FullBoar.Examples.OverInjection.Reference.Model;

namespace FullBoar.Examples.OverInjection.Reference.Services
{
    public interface IOverdraftService
    {
        void ApplyPenalty(Account account);
    }
}
