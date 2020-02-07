using FullBoar.Examples.OverInjection.RefactoringDemo.Model;

namespace FullBoar.Examples.OverInjection.RefactoringDemo.Services
{
    public interface IBouncedCheckService
    {
        void ApplyPenalty(Account account);
    }
}
