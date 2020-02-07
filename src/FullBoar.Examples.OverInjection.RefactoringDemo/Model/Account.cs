using System;

namespace FullBoar.Examples.OverInjection.RefactoringDemo.Model
{
    public class Account : IAccount
    {
        #region Member Variables
        private readonly bool _allowOverdrafts;

        private int _balance;
        #endregion

        #region Constructor
        public Account(bool allowOverdrafts)
        {
            _allowOverdrafts = allowOverdrafts;
        }
        #endregion

        #region Methods
        public ProcessResult Process(Deposit deposit)
        {
            ProcessResultBuilder builder = new ProcessResultBuilder();

            try
            {
                if(!deposit.IsValid)
                    builder.AddErrorMessage("Invalid deposit");

                if(!builder.HasErrorMessages)
                    _balance += deposit.Amount;

                return builder.UsingBalance(_balance);
            }
            catch (Exception ex)
            {
                return builder
                    .WithException(ex)
                    .UsingBalance(_balance);
            }
        }

        public ProcessResult Process(Withdrawal withdrawal)
        {
            ProcessResultBuilder builder = new ProcessResultBuilder();

            try
            {
                if(!withdrawal.IsValid)
                    builder.AddErrorMessage("Invalid withdrawal");

                if(!HasSufficientFunds(withdrawal.Amount))
                    builder.AddErrorMessage("You cannot withdraw more than is in the account");

                if(!builder.HasErrorMessages)
                    _balance -= withdrawal.Amount;

                return builder.UsingBalance(_balance);
            }
            catch (Exception ex)
            {
                return builder
                    .WithException(ex)
                    .UsingBalance(_balance);
            }
        }

        /*
        public ProcessResult Process(Check check)
        {
            ProcessResultBuilder builder = new ProcessResultBuilder();

            try
            {
                if(!check.IsValid)
                    builder.AddErrorMessage("Invalid check");

                if(!(HasSufficientFunds(check.Amount) || _allowOverdrafts))
                    builder
                        .AddErrorMessage("Check bounced, Check bounced, insufficient founds an overdrafts are not allowed")
                        .SetStatus(ProcessStatus.Bounced);

                if(builder.HasErrorMessages)
                    return builder.UsingBalance(_balance);

                _balance -= check.Amount;

                if(_balance < 0)
                    builder.SetStatus(ProcessStatus.OverWithdrawal);

                return builder.UsingBalance(_balance);
            }
            catch (Exception ex)
            {
                return builder
                    .WithException(ex)
                    .UsingBalance(_balance);
            }
        }
        */

        public ProcessResult Process(Check check)
        {
            ProcessResultBuilder builder = new ProcessResultBuilder();

            if (!(HasSufficientFunds(check.Amount) || _allowOverdrafts))
                builder
                    .AddErrorMessage("Check bounced, Check bounced, insufficient founds an overdrafts are not allowed")
                    .SetStatus(ProcessStatus.Bounced);

            if (builder.HasErrorMessages)
                return builder.UsingBalance(_balance);

            _balance -= check.Amount;

            if (_balance < 0)
                builder.SetStatus(ProcessStatus.OverWithdrawal);

            return builder.UsingBalance(_balance);
        }

        public ProcessResult Process(Fee fee)
        {
            ProcessResultBuilder builder = new ProcessResultBuilder();

            try
            {
                if(!fee.IsValid)
                    builder.AddErrorMessage("Invalid fee");

                if(!builder.HasErrorMessages)
                    _balance -= fee.Amount;

                return builder.UsingBalance(_balance);
            }
            catch (Exception ex)
            {
                return builder
                    .WithException(ex)
                    .UsingBalance(_balance);
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
        #endregion
    }
}
