using ChatChallenge.ClientSide.Models;
using ChatChallenge.ClientSide.Services.Interfaces;
using ChatChallenge.Resources.Constants;
using ChatChallenge.Resources.Enumerators;
using System;
using System.Net.Sockets;
using System.Text;

namespace ChatChallenge.ClientSide.Services
{
    public class NicknameValidateService : INicknameValidateService
    {
        private readonly byte[] _buffer;

        public NicknameValidateService()
        {
            _buffer = new byte[1024];
        }

        public void ValidateNickname(Client client, NetworkStream stream)
        {
            bool isValidUsername = false;

            while (!isValidUsername)
            {
                byte[] messageBuffer = Encoding.UTF8.GetBytes($"{ChatAction.ValidateNickname}{ChatChallengeConstants.MESSAGE_LIMITER}{client.Nickname}");
                stream.Write(messageBuffer, 0, messageBuffer.Length);

                int length = stream.Read(_buffer, 0, _buffer.Length);
                string responseServer = Encoding.UTF8.GetString(_buffer, 0, length);

                string[] splitMessage = responseServer.Split(ChatChallengeConstants.MESSAGE_LIMITER);

                Console.WriteLine($"*** {splitMessage[1]}");

                if (bool.Parse(splitMessage[0]))
                    isValidUsername = true;
                else
                {
                    string nicknameClient = Console.ReadLine();
                    client.UpdateUsername(nicknameClient);
                }
            }
        }
    }
}
