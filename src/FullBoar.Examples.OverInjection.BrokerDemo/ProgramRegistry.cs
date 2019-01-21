using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.BrokerDemo.Model;
using FullBoar.Examples.OverInjection.BrokerDemo.Services;
using NodaTime;
using Serilog;
using StructureMap;

namespace FullBoar.Examples.OverInjection.BrokerDemo
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
            For<INotificationService>().Singleton();

            For<ILogger>().Use(logger);
            For<IClock>().Use(SystemClock.Instance);

            For<IAccount>()
                .Use<Account>()
                .Ctor<bool>("allowOverdrafts").Is(true);
        }
        #endregion
    }
}
