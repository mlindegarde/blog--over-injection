using FullBoar.Examples.OverInjection.CombinedDemo.Interceptors;
using FullBoar.Examples.OverInjection.CombinedDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.CombinedDemo.Model;
using FullBoar.Examples.OverInjection.CombinedDemo.Services;
using NodaTime;
using Serilog;
using StructureMap;
using StructureMap.DynamicInterception;

namespace FullBoar.Examples.OverInjection.CombinedDemo
{
    public class ProgramRegistry : Registry
    {
        #region Constructor
        public ProgramRegistry(ILogger logger)
        {
            Scan(
                scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith("FullBoar"));
                    scanner.WithDefaultConventions();
                    scanner.AddAllTypesOf<ISubscriber>();
                });

            For<IMessageBroker>().Singleton();

            For<IOverdraftService>().Singleton();
            For<IBouncedCheckService>().Singleton();
            For<INotificationService>().Singleton();

            For<ILogger>().Use(logger);
            For<IClock>().Use(SystemClock.Instance);

            For<IAccount>()
                .Use<Account>()
                .Ctor<bool>("allowOverdrafts").Is(true)
                .InterceptWith(
                    new DynamicProxyInterceptor<IAccount>(
                        new IInterceptionBehavior[]
                        {

                            new ExceptionLoggingInterceptor(logger),
                            new TransactionValidationInterceptor(logger),
                        }));
        }
        #endregion
    }
}
