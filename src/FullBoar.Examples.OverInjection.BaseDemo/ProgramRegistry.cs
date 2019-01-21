using FullBoar.Examples.OverInjection.BaseDemo.Model;
using Serilog;
using StructureMap;

namespace FullBoar.Examples.OverInjection.BaseDemo
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
                });

            For<ILogger>().Use(logger);

            For<IAccount>()
                .Use<Account>()
                .Ctor<bool>("allowOverdrafts").Is(true);
        }
        #endregion
    }
}
