using FullBoar.Examples.OverInjection.AopDemo.Model;

namespace FullBoar.Examples.OverInjection.AopDemo.Services
{
    public interface ICheckValidatorService
    {
        bool IsValid(Check check);
    }
}
