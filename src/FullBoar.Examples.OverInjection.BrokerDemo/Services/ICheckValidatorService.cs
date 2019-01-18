using FullBoar.Examples.OverInjection.BrokerDemo.Model;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Services
{
    public interface ICheckValidatorService
    {
        bool IsValid(Check check);
    }
}
