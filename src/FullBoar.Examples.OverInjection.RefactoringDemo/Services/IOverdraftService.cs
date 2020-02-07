using FullBoar.Examples.OverInjection.RefactoringDemo.Model;

namespace FullBoar.Examples.OverInjection.RefactoringDemo.Services
{
    public interface IOverdraftService
    {
        void ApplyPenalty(Account account);
    }
}
