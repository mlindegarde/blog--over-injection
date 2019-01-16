using FullBoar.Examples.OverInjection.Domain.Model;

namespace FullBoar.Examples.OverInjection.Domain.Services
{
    public interface IAccountService
    {
        void Deposit(Account account, int amount);
        void Withdraw(Account account, int amount);

        void ProcessCheck(Account account, Check check);
    }
}
