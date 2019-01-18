using FullBoar.Examples.OverInjection.BaseDemo.Model;

namespace FullBoar.Examples.OverInjection.BaseDemo.Services
{
    public class BouncedCheckService : IBouncedCheckService
    {
        #region Constants
        private const int BouncedCheckFee = 100;
        #endregion

        #region IBouncedCheckImplementation
        public void ApplyPenalty(Account account)
        {
            account.Process(new Withdrawal(BouncedCheckFee));
        }
        #endregion
    }
}
