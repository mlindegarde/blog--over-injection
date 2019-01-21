using FullBoar.Examples.OverInjection.CombinedDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.CombinedDemo.Messaging.Events;
using NodaTime;

namespace FullBoar.Examples.OverInjection.CombinedDemo.Model
{
    public class Account : IAccount
    {
        #region Member Variables
        private readonly bool _allowOverdrafts;

        private readonly IMessageBroker _broker;
        private readonly IClock _clock;

        private int _balance;
        #endregion

        #region Constructor
        public Account(
            bool allowOverdrafts,
            IMessageBroker broker,
            IClock clock)
        {
            _allowOverdrafts = allowOverdrafts;
            _broker = broker;
            _clock = clock;
        }
        #endregion

        #region Methods
        public void Process(Deposit deposit)
        {
            _balance += deposit.Amount;
        }

        public void Process(Withdrawal withdrawal)
        {
            if(!HasSufficientFunds(withdrawal.Amount))
            {
                Publish<WithdrawalDeclined,Withdrawal>(withdrawal);
                return;
            }

            _balance -= withdrawal.Amount;
        }

        public void Process(Check check)
        {
            if(!(HasSufficientFunds(check.Amount) || _allowOverdrafts))
            {
                Publish<CheckBounced,Check>(check);
                return;
            }

            _balance -= check.Amount;

            if(_balance < 0)
                Publish<AccountOverWithdrawn,Check>(check);
        }

        public void Process(Fee fee)
        {
            _balance -= fee.Amount;

            Publish<FeeAssessed, Fee>(fee);
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

        private void Publish<TEvent,TTransaction>(TTransaction transaction)
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
