using ChatChallenge.ServerSide.Services;
using ChatChallenge.ServerSide.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatChallenge.ServerSide
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            RegisterServices();

            IServerStartupService _serverStartupService = _serviceProvider.GetService<IServerStartupService>();
            _serverStartupService.Initialize();

            Console.WriteLine("Server started...");

            Console.ReadKey();
        }

        private static void RegisterServices()
        {
            ServiceCollection collection = new ServiceCollection();
            collection.AddSingleton<TcpClientHandler>();
            collection.AddScoped<IServerStartupService, ServerStartupService>();
            collection.AddScoped<IServerActionHandler, ServerActionHandler>();
            collection.AddScoped<INicknameValidateService, NicknameValidateService>();

            _serviceProvider = collection.BuildServiceProvider();
        }
    }
}
