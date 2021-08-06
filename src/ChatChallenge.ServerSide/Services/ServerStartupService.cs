using ChatChallenge.Resources.Constants;
using ChatChallenge.ServerSide.Services.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatChallenge.ServerSide.Services
{
    public class ServerStartupService : IServerStartupService
    {
        private static TcpListener _tcpListener;
        private readonly TcpClientHandler _tcpClientHandler;

        private readonly IServerActionHandler _serverActionHandler;

        public ServerStartupService(
            IServerActionHandler serverActionHandler,
            TcpClientHandler tcpClientHandler)
        {
            _tcpClientHandler = tcpClientHandler;
            _serverActionHandler = serverActionHandler;
        }

        public void Initialize()
        {
            IPAddress ipAddress = IPAddress.Parse(ChatChallengeConstants.SERVER_IP);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, ChatChallengeConstants.SERVER_PORT);

            _tcpListener = new TcpListener(ipEndPoint);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptIncomingTcpclient), _tcpListener);
        }

        private void AcceptIncomingTcpclient(IAsyncResult State)
        {
            TcpListener listener = (TcpListener)State.AsyncState;
            TcpClient client = listener.EndAcceptTcpClient(State);

            _tcpClientHandler.AddClient(client);

            Console.WriteLine($"Connected client: {client.Client.RemoteEndPoint}");

            Thread thread = new Thread(new ParameterizedThreadStart(_serverActionHandler.Handle));
            thread.Start(client);

            listener.BeginAcceptTcpClient(new AsyncCallback(AcceptIncomingTcpclient), listener);
        }
    }
}
