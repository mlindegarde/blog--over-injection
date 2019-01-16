using System;
using FullBoar.Examples.OverInjection.Domain.Model;
using Serilog;

namespace FullBoar.Examples.OverInjection.Domain.Services
{
    public class AccountService : IAccountService
    {
        #region Member Variables
        private readonly ICheckValidatorService _checkValidatorSvc;
        private readonly IOverdraftService _overdraftSvc;
        private readonly INotificationService _notificationSvc;
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public AccountService(
            ICheckValidatorService checkValidatorSvc,
            IOverdraftService overdraftSvc,
            INotificationService notificationSvc,
            ILogger logger)
        {
            _checkValidatorSvc = checkValidatorSvc;
            _overdraftSvc = overdraftSvc;
            _notificationSvc = notificationSvc;
            _logger = logger;
        }
        #endregion

        #region IAccountService Implementation
        public void Deposit(Account account, int amount)
        {
            if(amount <= 0)
            {
                _logger.Error("Invalid deposit amount {Amount}", amount);
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Invalid deposit amount");
            }

            account.Balance += amount;
        }

        public void Withdraw(Account account, int amount)
        {
            if(account.Balance < amount)
            {
                _logger.Error("You cannot withdraw more than is in the account");
                return;
            }

            account.Balance -= amount;
        }

        public void ProcessCheck(Account account, Check check)
        {
            if(account == null)
                throw new ArgumentNullException(nameof(Account), "You must provide an account");

            if(!_checkValidatorSvc.IsValid(check))
            {
                _notificationSvc.SendNotification($"Invalid check: {check.Number}");
                _logger.Information("Invalid check: {@Check}", check);
                return;
            }

            account.Balance -= check.Amount;

            if(account.Balance < 0)
            {
                _notificationSvc.SendNotification("Account over withdrawn");
                _logger.Information("Account over withdrawn");
                _overdraftSvc.ApplyPenalty(account);
            }
        }
        #endregion
    }
}
