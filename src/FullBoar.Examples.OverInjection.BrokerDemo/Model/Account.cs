using System;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Events;
using NodaTime;
using Serilog;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Model
{
    public class Account : IAccount
    {
        #region Member Variables
        private readonly bool _allowOverdrafts;
        private readonly ILogger _logger;

        private readonly IMessageBroker _broker;
        private readonly IClock _clock;

        private int _balance;
        #endregion

        #region Constructor
        public Account(
            bool allowOverdrafts,
            IMessageBroker broker,
            IClock clock,
            ILogger logger)
        {
            _allowOverdrafts = allowOverdrafts;
            _broker = broker;
            _clock = clock;
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
                    throw new ArgumentException("Invalid transaction");
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

                if (!HasSufficientFunds(withdrawal.Amount))
                {
                    Publish<WithdrawalDeclined, Withdrawal>(withdrawal);
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

                if (!(HasSufficientFunds(check.Amount) || _allowOverdrafts))
                {
                    Publish<CheckBounced, Check>(check);
                    return;
                }

                _balance -= check.Amount;

                if (_balance < 0)
                    Publish<AccountOverWithdrawn, Check>(check);
            }
            catch (Exception ex)
            {
                _logger.Error("The following exception is being logged: {Message}", ex.Message);
                throw;
            }
        }

        public void Process(Fee fee)
        {
            try
            {
                if (!fee.IsValid)
                {
                    _logger.Error("Invalid transaction {@Transaction}", fee);
                    throw new ArgumentException("Invalid transaction");
                }

                _balance -= fee.Amount;

                Publish<FeeAssessed,Fee>(fee);
            }
            catch (Exception ex)
            {
                _logger.Error("The following exception is being logged: {Message}", ex.Message);
                throw;
            }
        }

        public int GetBalance()
        {
            return _balance;
        }
        #endregion

        #region Utiltiy Methods
        private bool HasSufficientFunds(int amount)
        {
            return _balance >= amount;
        }

        private void Publish<TEvent, TTransaction>(TTransaction transaction)
            where TTransaction : ITransaction, new()
            where TEvent : ITransactionEvent<TTransaction>, new()
        {
            _broker.Publish(
                new TEvent
                {
                    Account = this,
                    Transaction = transaction,
                    CreatedAt = _clock.GetCurrentInstant()
                });
        }
        #endregion
    }
}
