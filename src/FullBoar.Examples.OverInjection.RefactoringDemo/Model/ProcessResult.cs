using System;
using System.Collections.Generic;
using System.Linq;

namespace FullBoar.Examples.OverInjection.RefactoringDemo.Model
{
    public enum ProcessStatus
    {
        Undefined = 0,
        Successful = 1,
        Bounced = 2,
        Rejected = 3,
        OverWithdrawal = 4
    }

    public class ProcessResult
    {
        #region Properties
        public int NewBalance { get; set; }
        public ProcessStatus Status { get; set; }
        public List<string> ErrorMessages { get; set; }
        public Exception Exception { get; set; }
        #endregion
    }

    public class ProcessResultBuilder
    {
        #region Member Variables
        private readonly List<string> _errorMessages = new List<string>();

        private int? _newBalance;
        private ProcessStatus? _status;
        private Exception _exception;
        #endregion

        #region Properties
        public bool HasErrorMessages => _errorMessages?.Any() == true;
        #endregion

        #region Setter Methods
        public ProcessResultBuilder UsingBalance(int balance)
        {
            _newBalance = balance;
            return this;
        }

        public ProcessResultBuilder SetStatus(ProcessStatus status)
        {
            _status = status;
            return this;
        }

        public ProcessResultBuilder AddErrorMessage(string message)
        {
            _errorMessages.Add(message);
            _status ??= ProcessStatus.Rejected;
            return this;
        }

        public ProcessResultBuilder AddErrorMessages(List<string> messages)
        {
            _errorMessages.AddRange(messages);
            _status ??= ProcessStatus.Rejected;
            return this;
        }

        public ProcessResultBuilder WithException(Exception ex)
        {
            _exception = ex;
            return this;
        }
        #endregion

        #region Factory Method
        public ProcessResult Build()
        {
            if(!_errorMessages.Any() && _exception != null)
                _errorMessages.Add(_exception.Message);

            return
                new ProcessResult
                {
                    NewBalance = _newBalance ?? 0,
                    Status = _status ?? ProcessStatus.Successful,
                    Exception = _exception,
                    ErrorMessages = _errorMessages.Any()? _errorMessages : null
                };
        }
        #endregion

        #region Operator Overloads
        public static implicit operator ProcessResult(ProcessResultBuilder builder) => builder.Build();
        #endregion
    }
}
