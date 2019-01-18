using FullBoar.Examples.OverInjection.BaseDemo.Model;

namespace FullBoar.Examples.OverInjection.BaseDemo.Services
{
    public class OverdraftService : IOverdraftService
    {
        #region Constants
        private const int OverdraftFee = 35;
        #endregion

        #region IOverdraftService Implementation
        public void ApplyPenalty(Account account)
        {
            account.Process(new Withdrawal(OverdraftFee));
        }
        #endregion
    }
}
