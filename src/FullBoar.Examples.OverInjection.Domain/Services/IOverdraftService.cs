using FullBoar.Examples.OverInjection.Domain.Model;

namespace FullBoar.Examples.OverInjection.Domain.Services
{
    public interface IOverdraftService
    {
        void ApplyPenalty(Account account);
    }
}
