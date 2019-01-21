﻿using System;
using System.Linq;
using FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker;
using FullBoar.Examples.OverInjection.BrokerDemo.Model;
using Serilog;
using StructureMap;

namespace FullBoar.Examples.OverInjection.BrokerDemo
{
    class Program
    {
        private IContainer _container;
        private ILogger _logger;

        private void Init()
        {
            _logger =
                new LoggerConfiguration()
                    .WriteTo.Console()
                    .MinimumLevel.Verbose()
                    .CreateLogger();

            _container = new Container(new ProgramRegistry(_logger));

            _container
                .GetAllInstances<ISubscriber>()
                .ToList()
                .ForEach(
                    s => s.Subscribe());
        }

        private void RunOverdraft()
        {
            IAccount account = _container.GetInstance<IAccount>();
            account.Process(new Deposit(100));

            _logger.Information("Starting Overdraft Example...");
            _logger.Information($"Opening balance: {account.GetBalance()}");

            account.Process(new Withdrawal(50));
            account.Process(new Check(1, 75));

            _logger.Information($"Closing balance: {account.GetBalance()}");
            _logger.Information($"Done{Environment.NewLine}");
        }

        private void RunException()
        {
            IAccount account = _container.GetInstance<IAccount>();
            account.Process(new Deposit(100));

            _logger.Information("Starting Exception Example...");
            _logger.Information($"Opening balance: {account.GetBalance()}");

            try
            {
                account.Process(new Deposit(-100));
            }
            finally
            {
                _logger.Information($"Closing balance: {account.GetBalance()}");
                _logger.Information($"Done.{Environment.NewLine}");
            }
        }

        static void Main()
        {
            try
            {
                Console.WriteLine($"Broker Demo {Environment.NewLine}");

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
