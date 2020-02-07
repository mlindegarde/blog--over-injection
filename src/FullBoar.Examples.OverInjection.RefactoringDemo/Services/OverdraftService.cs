using FullBoar.Examples.OverInjection.RefactoringDemo.Model;

namespace FullBoar.Examples.OverInjection.RefactoringDemo.Services
{
    public class OverdraftService : IOverdraftService
    {
        #region Constants
        private const int OverdraftFee = 25;
        #endregion

        #region IOverdraftService Implementation
        public void ApplyPenalty(Account account)
        {
            account.Process(new Fee(OverdraftFee));
        }
        #endregion
    }
}
