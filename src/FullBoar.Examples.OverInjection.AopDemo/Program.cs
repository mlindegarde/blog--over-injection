﻿using System;
using FullBoar.Examples.OverInjection.AopDemo.Interceptors;
using FullBoar.Examples.OverInjection.AopDemo.Model;
using FullBoar.Examples.OverInjection.AopDemo.Services;
using Serilog;
using StructureMap;
using StructureMap.DynamicInterception;

namespace FullBoar.Examples.OverInjection.AopDemo
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

                    config
                        .For<IAccountService>()
                        .InterceptWith(
                            new DynamicProxyInterceptor<IAccountService>(
                                new IInterceptionBehavior[]
                                {
                                    new ExceptionLoggingInterceptor(_logger)
                                }));
                });
        }

        private void RunOverdraft()
        {
            _logger.Information("Starting Overdraft Example...");
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
            _logger.Information($"Done.{Environment.NewLine}");
        }

        private void RunException()
        {
            _logger.Information("Starting Exception Example...");

            try
            {
                IAccountService accountSvc = _container.GetInstance<IAccountService>();

                Account account = new Account();

                accountSvc.Deposit(account, -100);
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
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception: {ex.Message}");
            }

            Console.Write("Press ENTER to exit: ");
            Console.ReadLine();
        }
    }
}
