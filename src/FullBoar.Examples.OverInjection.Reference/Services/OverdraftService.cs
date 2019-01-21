using FullBoar.Examples.OverInjection.Reference.Model;

namespace FullBoar.Examples.OverInjection.Reference.Services
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
