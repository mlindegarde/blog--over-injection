using System;
using FullBoar.Examples.OverInjection.BaseDemo.Model;
using Serilog;
using StructureMap;

namespace FullBoar.Examples.OverInjection.BaseDemo
{
    class Program
    {
        private readonly IContainer _container = new Container();

        private ILogger _logger;

        private void Init()
        {
            _logger = 
                new LoggerConfiguration()
                    .WriteTo.Console()
                    .MinimumLevel.Verbose()
                    .CreateLogger();

            _container.Configure(
                config =>
                {
                    config.Scan(
                        scanner =>
                        {
                            scanner.TheCallingAssembly();
                            scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(
                                assembly => assembly.FullName.StartsWith("FullBoar"));
                            scanner.WithDefaultConventions();
                        });

                    config
                        .For<ILogger>()
                        .Use(_logger);
                });
        }

        private void RunOverdraft()
        {
            _logger.Information("Starting Overdraft Example...");

            Account account = _container.GetInstance<Account>();

            account.Process(new Deposit(100));
            account.Process(new Withdrawal(100));
            account.Process(new Check(1, 75));

            _logger.Information($"Done.{Environment.NewLine}");
        }

        private void RunException()
        {
            _logger.Information("Starting Exception Example...");

            try
            {
                _container.GetInstance<Account>().Process(new Deposit(-100));
            }
            finally
            {
                _logger.Information($"Done.{Environment.NewLine}");
            }
        }

        static void Main()
        {
            try
            {
                Program program = new Program();

                program.Init();
                program.RunOverdraft();
                program.RunException();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unhandled exception: {ex.Message}");
            }

            Console.Write("Press ENTER to exit: ");
            Console.ReadLine();
        }
    }
}
