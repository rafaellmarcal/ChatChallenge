using ChatChallenge.ClientSide.Services;
using ChatChallenge.ClientSide.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatChallenge.ClientSide
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            RegisterServices();

            IClientStartupService _clientStartupService = _serviceProvider.GetService<IClientStartupService>();
            _clientStartupService.Initialize();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IClientStartupService, ClientStartupService>();
            collection.AddScoped<INicknameValidateService, NicknameValidateService>();
            collection.AddScoped<IServerMessageReceiver, ServerMessageReceiver>();
            collection.AddScoped<ISendingMessageService, SendingMessageService>();

            _serviceProvider = collection.BuildServiceProvider();
        }
    }
}
