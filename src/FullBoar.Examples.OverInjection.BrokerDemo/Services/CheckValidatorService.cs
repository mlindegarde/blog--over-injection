using FullBoar.Examples.OverInjection.BrokerDemo.Model;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Services
{
    public class CheckValidatorService : ICheckValidatorService
    {
        #region ICheckValidatorService Implementation
        public bool IsValid(Check check) => true;
        #endregion
    }
}
