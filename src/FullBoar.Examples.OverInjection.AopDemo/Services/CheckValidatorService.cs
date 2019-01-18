using FullBoar.Examples.OverInjection.AopDemo.Model;

namespace FullBoar.Examples.OverInjection.AopDemo.Services
{
    public class CheckValidatorService : ICheckValidatorService
    {
        #region ICheckValidatorService Implementation
        public bool IsValid(Check check) => true;
        #endregion
    }
}
