using FullBoar.Examples.OverInjection.AopDemo.Model;

namespace FullBoar.Examples.OverInjection.AopDemo.Services
{
    public interface IAccountService
    {
        void Deposit(Account account, int amount);
        void Withdraw(Account account, int amount);

        void ProcessCheck(Account account, Check check);
    }
}
