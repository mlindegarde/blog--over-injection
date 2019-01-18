using FullBoar.Examples.OverInjection.BrokerDemo.Model;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Services
{
    public interface IAccountService
    {
        void Deposit(Account account, int amount);
        void Withdraw(Account account, int amount);

        void ProcessCheck(Account account, Check check);
    }
}
