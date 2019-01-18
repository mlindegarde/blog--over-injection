using System;
using FullBoar.Examples.OverInjection.BaseDemo.Services;
using Serilog;

namespace FullBoar.Examples.OverInjection.BaseDemo.Model
{
    public class Account
    {
        #region Member Variables
        private readonly bool _allowOverdrafts;

        private readonly INotificationService _notificationSvc;
        private readonly IOverdraftService _overdraftSvc;
        private readonly IBouncedCheckService _bouncedCheckSvc;
        private readonly ILogger _logger;

        private int _balance;
        #endregion

        #region Constructor
        public Account(
            bool allowOverdrafts,
            INotificationService notificationSvc,
            IOverdraftService overdraftSvc,
            IBouncedCheckService bouncedCheckSvc,
            ILogger logger)
        {
            _allowOverdrafts = allowOverdrafts;

            _notificationSvc = notificationSvc;
            _overdraftSvc = overdraftSvc;
            _bouncedCheckSvc = bouncedCheckSvc;

            _logger = logger;
        }
        #endregion

        #region Methods
        public void Process(Deposit deposit)
        {
            try
            {
                if (!deposit.IsValid)
                {
                    _logger.Error("Invalid transaction {@Transaction}", deposit);
                    throw new ArgumentOutOfRangeException(nameof(deposit), deposit, "Invalid transaction");
                }

                _balance += deposit.Amount;
            }
            catch (Exception ex)
            {
                _logger.Error("The following exception is being logged: {Message}", ex.Message);
                throw;
            }
        }

        public void Process(Withdrawal withdrawal)
        {
            try
            {
                if (!withdrawal.IsValid)
                {
                    _logger.Error("Invalid transaction {@Transaction}", withdrawal);
                    throw new ArgumentOutOfRangeException(nameof(withdrawal), withdrawal, "Invalid transaction");
                }

                if (HasSufficientFunds(withdrawal.Amount))
                {
                    _logger.Error("You cannot withdraw more than is in the account");
                    return;
                }

                _balance -= withdrawal.Amount;
            }
            catch (Exception ex)
            {
                _logger.Error("The following exception is being logged: {Message}", ex.Message);
                throw;
            }
        }

        public void Process(Check check)
        {
            try
            {
                if (!check.IsValid)
                {
                    _logger.Error("Invalid transaction {@Transaction}", check);
                    throw new ArgumentOutOfRangeException(nameof(check), check, "Invalid transaction");
                }

                if(!(HasSufficientFunds(check.Amount) || _allowOverdrafts))
                {
                    _notificationSvc.SendNotification("Check bounced, insufficient founds an overdrafts are not allowed");
                    _logger.Information("Check bounced: insufficient founds");
                    _bouncedCheckSvc.ApplyPenalty(this);

                    return;
                }

                if(HasSufficientFunds(check.Amount) || _allowOverdrafts)
                    _balance -= check.Amount;

                if (_balance < 0)
                {
                    _notificationSvc.SendNotification("Account over withdrawn");
                    _logger.Information("Account over withdrawn");
                    _overdraftSvc.ApplyPenalty(this);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("The following exception is being logged: {Message}", ex.Message);
                throw;
            }
        }
        #endregion

        #region Utiltiy Methods
        private bool HasSufficientFunds(int amount)
        {
            return _balance >= amount;
        }
        #endregion
    }
}
