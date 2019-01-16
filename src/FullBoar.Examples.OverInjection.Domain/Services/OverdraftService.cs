using FullBoar.Examples.OverInjection.Domain.Model;

namespace FullBoar.Examples.OverInjection.Domain.Services
{
    public class OverdraftService : IOverdraftService
    {
        #region Constants
        private const int OverdraftFee = 35;
        #endregion

        #region IOverdraftService Implementation
        public void ApplyPenalty(Account account)
        {
            account.Balance -= OverdraftFee;
        }
        #endregion
    }
}
