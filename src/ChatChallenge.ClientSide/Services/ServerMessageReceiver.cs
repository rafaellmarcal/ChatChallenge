using ChatChallenge.ClientSide.Services.Interfaces;
using System;
using System.Net.Sockets;
using System.Text;

namespace ChatChallenge.ClientSide.Services
{
    public class ServerMessageReceiver : IServerMessageReceiver
    {
        private readonly byte[] _buffer;

        public ServerMessageReceiver()
        {
            _buffer = new byte[1024];
        }

        public void WriteMessageReceived(object tcpClientObject)
        {
            TcpClient client = (TcpClient)tcpClientObject;
            NetworkStream stream = client.GetStream();

            while (true)
            {
                if (stream.DataAvailable)
                {
                    int length = stream.Read(_buffer, 0, _buffer.Length);
                    string receiveMessage = Encoding.UTF8.GetString(_buffer, 0, length);

                    Console.WriteLine($"{receiveMessage}");
                    Console.Write(">:");
                }
            }
        }
    }
}
