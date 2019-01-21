using FullBoar.Examples.OverInjection.AopDemo.Services;

namespace FullBoar.Examples.OverInjection.AopDemo.Model
{
    public class Account : IAccount
    {
        #region Member Variables
        private readonly bool _allowOverdrafts;

        private readonly INotificationService _notificationSvc;
        private readonly IOverdraftService _overdraftSvc;
        private readonly IBouncedCheckService _bouncedCheckSvc;

        private int _balance;
        #endregion

        #region Constructor
        public Account(
            bool allowOverdrafts,
            INotificationService notificationSvc,
            IOverdraftService overdraftSvc,
            IBouncedCheckService bouncedCheckSvc)
        {
            _allowOverdrafts = allowOverdrafts;

            _notificationSvc = notificationSvc;
            _overdraftSvc = overdraftSvc;
            _bouncedCheckSvc = bouncedCheckSvc;
        }
        #endregion

        #region Methods
        public void Process(Deposit deposit)
        {
            _balance += deposit.Amount;
        }

        public void Process(Withdrawal withdrawal)
        {
            if (!HasSufficientFunds(withdrawal.Amount))
            {
                _notificationSvc.SendNotification("You cannot withdraw more than is in the account");
                return;
            }

            _balance -= withdrawal.Amount;
        }

        public void Process(Check check)
        {
            if (!(HasSufficientFunds(check.Amount) || _allowOverdrafts))
            {
                _notificationSvc.SendNotification("Check bounced, insufficient founds an overdrafts are not allowed");
                _bouncedCheckSvc.ApplyPenalty(this);

                return;
            }

            _balance -= check.Amount;

            if (_balance < 0)
            {
                _notificationSvc.SendNotification("Account over withdrawn");
                _overdraftSvc.ApplyPenalty(this);
            }
        }

        public void Process(Fee fee)
        {
            _balance -= fee.Amount;

            _notificationSvc.SendNotification($"Fee assessed: {fee.Amount}");
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
        #endregion
    }
}
