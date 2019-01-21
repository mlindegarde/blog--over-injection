using FullBoar.Examples.OverInjection.Reference.Model;

namespace FullBoar.Examples.OverInjection.Reference.Services
{
    public class BouncedCheckService : IBouncedCheckService
    {
        #region Constants
        private const int BouncedCheckFee = 50;
        #endregion

        #region IBouncedCheckImplementation
        public void ApplyPenalty(Account account)
        {
            account.Process(new Fee(BouncedCheckFee));
        }
        #endregion
    }
}
