using System;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Events;
using FullBoar.Examples.OverInjection.BrokerDemo.Model;
using Serilog;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Services
{
    public class AccountService : IAccountService
    {
        #region Member Variables
        private readonly IMessageBroker _broker;
        private readonly ICheckValidatorService _checkValidatorSvc;
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public AccountService(
            IMessageBroker broker,
            ICheckValidatorService checkValidatorSvc,
            ILogger logger)
        {
            _broker = broker;
            _checkValidatorSvc = checkValidatorSvc;
            _logger = logger;
        }
        #endregion

        #region IAccountService Implementation
        public void Deposit(Account account, int amount)
        {
            try
            {
                if (amount <= 0)
                {
                    _logger.Error("Invalid deposit amount {Amount}", amount);
                    throw new ArgumentOutOfRangeException(nameof(amount), amount, "Invalid deposit amount");
                }

                account.Balance += amount;
            }
            catch (Exception ex)
            {
                _logger.Error("The following exception is being logged: {Message}", ex.Message);
                throw;
            }
        }

        public void Withdraw(Account account, int amount)
        {
            try
            {
                if (account.Balance < amount)
                {
                    _logger.Error("You cannot withdraw more than is in the account");
                    return;
                }

                account.Balance -= amount;
            }
            catch (Exception ex)
            {
                _logger.Error("The following exception is being logged: {Message}", ex.Message);
                throw;
            }
        }

        public void ProcessCheck(Account account, Check check)
        {
            try
            {
                if (account == null)
                    throw new ArgumentNullException(nameof(Account), "You must provide an account");

                if (!_checkValidatorSvc.IsValid(check))
                {
                    _broker.Publish(new CheckFailedValidation {Check = check});
                    return;
                }

                account.Balance -= check.Amount;

                if (account.Balance < 0)
                    _broker.Publish(new AccountOverWithdrawn {Account = account});
            }
            catch (Exception ex)
            {
                _logger.Error("The following exception is being logged: {Message}", ex.Message);
                throw;
            }
        }
        #endregion
    }
}
