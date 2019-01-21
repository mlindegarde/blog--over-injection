using FullBoar.Examples.OverInjection.AopDemo.Interceptors;
using FullBoar.Examples.OverInjection.AopDemo.Model;
using Serilog;
using StructureMap;
using StructureMap.DynamicInterception;

namespace FullBoar.Examples.OverInjection.AopDemo
{
    class ProgramRegistry : Registry
    {
        public ProgramRegistry(ILogger logger)
        {
            Scan(
                scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith("FullBoar"));
                    scanner.WithDefaultConventions();
                });

            For<ILogger>().Use(logger);

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
    }
}
