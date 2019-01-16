using System;
using FullBoar.Examples.OverInjection.Domain.Model;
using FullBoar.Examples.OverInjection.Domain.Services;
using Serilog;
using StructureMap;

namespace FullBoar.Examples.OverInjection.AopDemo
{
    class Program
    {
        private readonly IContainer _container = new Container();

        private void Init()
        {
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
                        .Use(
                            new LoggerConfiguration()
                                .WriteTo.Console()
                                .MinimumLevel.Verbose()
                                .CreateLogger());
                });
        }

        private void RunOverdraft()
        {
            try
            {
                IAccountService accountSvc = _container.GetInstance<IAccountService>();

                Account account = new Account();

                accountSvc.Deposit(account, 100);
                accountSvc.Withdraw(account, 50);

                accountSvc.ProcessCheck(
                    account,
                    new Check
                    {
                        Number = 1,
                        Amount = 75
                    });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void RunException()
        {
            try
            {
                IAccountService accountSvc = _container.GetInstance<IAccountService>();

                Account account = new Account();

                accountSvc.Deposit(account, -100);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();

            program.Init();
            program.RunOverdraft();
            program.RunException();

            Console.Write("Press ENTER to exit: ");
            Console.ReadLine();
        }
    }
}
