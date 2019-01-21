using Serilog;
using StructureMap.DynamicInterception;

namespace FullBoar.Examples.OverInjection.FinalDemo.Interceptors
{
    public class ExceptionLoggingInterceptor : ISyncInterceptionBehavior
    {
        #region Member Variables
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public ExceptionLoggingInterceptor(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region ISyncInterceptionBehavior Implementation
        public IMethodInvocationResult Intercept(ISyncMethodInvocation invocation)
        {
            IMethodInvocationResult result = invocation.InvokeNext();

            if(result.Exception != null)
            {
                _logger.Error("The following exception is being logged: {Message}", result.Exception.Message);
            }

            return result;
        }
        #endregion
    }
}
