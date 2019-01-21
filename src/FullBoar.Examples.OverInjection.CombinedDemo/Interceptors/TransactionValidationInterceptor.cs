using System;
using System.Collections.Generic;
using System.Linq;
using FullBoar.Examples.OverInjection.CombinedDemo.Model;
using Serilog;
using StructureMap.DynamicInterception;

namespace FullBoar.Examples.OverInjection.CombinedDemo.Interceptors
{
    public class TransactionValidationInterceptor : ISyncInterceptionBehavior
    {
        #region Member Variables
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public TransactionValidationInterceptor(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region ISyncInterceptionBehavior Implementation
        public IMethodInvocationResult Intercept(ISyncMethodInvocation invocation)
        {
            List<ITransaction> invalidTransactions = 
                invocation.Arguments
                    .Where(arg => (arg.Value as ITransaction)?.IsValid == false)
                    .Select(arg => (ITransaction)arg.Value)
                    .ToList();

            invalidTransactions.ForEach(
                t =>
                {
                    _logger.Error("Invalid transaction {@Transaction}", t);
                });
            
            return invalidTransactions.Any()
                ? invocation.CreateExceptionResult(
                    new ArgumentException("Invalid transaction"))
                : invocation.InvokeNext();
        }
        #endregion
    }
}
