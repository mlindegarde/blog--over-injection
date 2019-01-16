using FullBoar.Examples.OverInjection.Domain.Model;

namespace FullBoar.Examples.OverInjection.Domain.Services
{
    public class CheckValidatorService : ICheckValidatorService
    {
        #region ICheckValidatorService Implementation
        public bool IsValid(Check check) => true;
        #endregion
    }
}
