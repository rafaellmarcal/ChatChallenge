using ChatChallenge.ClientSide.Models;
using ChatChallenge.ClientSide.Services.Interfaces;
using ChatChallenge.Resources.Constants;
using ChatChallenge.Resources.Enumerators;
using System;
using System.Net.Sockets;
using System.Text;

namespace ChatChallenge.ClientSide.Services
{
    public class SendingMessageService : ISendingMessageService
    {
        public SendingMessageService() { }

        public void Send(Client client, NetworkStream stream)
        {
            while (true)
            {
                Console.Write(">:");
                string message = Console.ReadLine();

                string temp = BuildMessage(client, message);

                byte[] messageBuffer = Encoding.UTF8.GetBytes(temp);
                stream.Write(messageBuffer, 0, messageBuffer.Length);
            }
        }

        private string BuildMessage(Client client, string message)
        {
            string builtText;

            switch (message)
            {
                case "/exit":
                    builtText = $"{ChatAction.Disconnect}{ChatChallengeConstants.MESSAGE_LIMITER}{client.Nickname}";
                    break;
                default:
                    builtText = $"{ChatAction.SendPublicMessage}{ChatChallengeConstants.MESSAGE_LIMITER}{client.Nickname}:{message}";
                    break;
            }

            return builtText;
        }
    }
}
