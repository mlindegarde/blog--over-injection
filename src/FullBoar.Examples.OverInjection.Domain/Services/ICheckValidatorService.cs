using FullBoar.Examples.OverInjection.Domain.Model;

namespace FullBoar.Examples.OverInjection.Domain.Services
{
    public interface ICheckValidatorService
    {
        bool IsValid(Check check);
    }
}
