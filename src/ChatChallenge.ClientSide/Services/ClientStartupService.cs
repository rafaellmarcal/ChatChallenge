using ChatChallenge.ClientSide.Models;
using ChatChallenge.ClientSide.Services.Interfaces;
using ChatChallenge.Resources.Constants;
using System;
using System.Net.Sockets;
using System.Threading;

namespace ChatChallenge.ClientSide.Services
{
    public class ClientStartupService : IClientStartupService
    {
        private readonly ISendingMessageService _sendingMessageService;
        private readonly IServerMessageReceiver _serverMessageReceiver;
        private readonly INicknameValidateService _nicknameValidateService;

        public ClientStartupService(
            IServerMessageReceiver serverMessageReceiver,
            ISendingMessageService sendingMessageService,
            INicknameValidateService nicknameValidateService)
        {
            _serverMessageReceiver = serverMessageReceiver;
            _nicknameValidateService = nicknameValidateService;
            _sendingMessageService = sendingMessageService;
        }

        public void Initialize()
        {
            Console.WriteLine("Welcome to our chat server. Please provide a nickname:");
            string nicknameClient = Console.ReadLine();

            Client client = new Client(nicknameClient, ChatChallengeConstants.SERVER_IP, ChatChallengeConstants.SERVER_PORT);

            StartConnectionToServer(client);
        }

        private void StartConnectionToServer(Client client)
        {
            TcpClient tcpClient = new TcpClient(client.IpAddress, client.Port);

            NetworkStream stream = tcpClient.GetStream();

            _nicknameValidateService.ValidateNickname(client, stream);

            Thread thread = new Thread(new ParameterizedThreadStart(_serverMessageReceiver.WriteMessageReceived));
            thread.Start(tcpClient);

            _sendingMessageService.Send(client, stream);
        }
    }
}
