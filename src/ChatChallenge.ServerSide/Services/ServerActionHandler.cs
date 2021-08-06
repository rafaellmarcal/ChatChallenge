using ChatChallenge.Resources.Constants;
using ChatChallenge.Resources.Enumerators;
using ChatChallenge.ServerSide.Services.Interfaces;
using System;
using System.Net.Sockets;
using System.Text;

namespace ChatChallenge.ServerSide.Services
{
    public class ServerActionHandler : IServerActionHandler
    {
        private byte[] _buffer;
        private readonly TcpClientHandler _tcpClientHandler;
        private readonly INicknameValidateService _nicknameValidateService;


        public ServerActionHandler(
            TcpClientHandler tcpClientHandler,
            INicknameValidateService nicknameValidateService)
        {
            _buffer = new byte[1024];
            _tcpClientHandler = tcpClientHandler;
            _nicknameValidateService = nicknameValidateService;
        }

        public void Handle(object tcpClientObject)
        {
            TcpClient client = tcpClientObject as TcpClient;

            if (client == null)
            {
                Console.WriteLine("Server error: Client could not be found.");
                return;
            }

            while (true)
            {
                try
                {
                    _buffer = new byte[1024];
                    NetworkStream stream = client.GetStream();
                    int messageBytes = stream.Read(_buffer, 0, _buffer.Length);

                    if (messageBytes != 0)
                    {
                        string messageFromClient = Encoding.UTF8.GetString(_buffer, 0, messageBytes);

                        DoAction(client, messageFromClient, stream);
                    }
                    else
                    {
                        client.Dispose();
                        break;
                    }
                }
                catch (Exception e)
                {
                    _tcpClientHandler.RemoveClient(client);
                    Console.WriteLine("Server error:" + e.ToString());
                    break;
                }
            }
        }

        private void DoAction(TcpClient client, string messageFromClient, NetworkStream stream)
        {
            (ChatAction chatAction, string message) = SplitMessageFromClient(messageFromClient);

            switch (chatAction)
            {
                case ChatAction.ValidateNickname:
                    string responseValidation = _nicknameValidateService.ValidateNickname(message);
                    Send(client, responseValidation, stream);
                    break;
                case ChatAction.SendPrivateMessage:
                    SendPrivateMessage(client, message);
                    break;
                case ChatAction.SendPublicMessage:
                    SendMessageToAllClients(client, message, stream);
                    break;
                default:
                    Disconnect(client, message);
                    break;
            }
        }

        private (ChatAction, string) SplitMessageFromClient(string message)
        {
            string[] splitMessage = message.Split(ChatChallengeConstants.MESSAGE_LIMITER);

            Enum.TryParse(splitMessage[0], out ChatAction chatAction);

            return (chatAction, splitMessage[1]);
        }

        private void SendPrivateMessage(TcpClient client, string message)
        {
            throw new NotImplementedException("Send private message is not available yet.");
        }

        private void SendMessageToAllClients(TcpClient client, string message, NetworkStream stream)
        {
            foreach (TcpClient otherClient in _tcpClientHandler.GetAllClients())
            {
                if (otherClient.Client.RemoteEndPoint != client.Client.RemoteEndPoint)
                    Send(otherClient, message, stream);
            }
        }

        private void Send(TcpClient client, string message, NetworkStream stream)
        {
            _buffer = Encoding.UTF8.GetBytes(message);

            stream = client.GetStream();
            stream.Write(_buffer, 0, _buffer.Length);
            stream.Flush();
        }

        private void Disconnect(TcpClient client, string message)
        {
            Console.WriteLine($"Disconnected client: {client.Client.RemoteEndPoint}");

            _tcpClientHandler.RemoveClient(client);
            _tcpClientHandler.RemoveNickname(message);
            client.Dispose();
        }
    }
}
