using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatChallenge.ServerSide.Services
{
    public class TcpClientHandler
    {
        private readonly List<string> _nicknames;
        private readonly List<TcpClient> _tcpClients;

        public TcpClientHandler()
        {
            _nicknames = new List<string>();
            _tcpClients = new List<TcpClient>();
        }

        public List<TcpClient> GetAllClients() => _tcpClients;

        public void AddClient(TcpClient client)
        {
            _tcpClients.Add(client);
        }

        public void RemoveClient(TcpClient client)
        {
            _tcpClients.Remove(client);
        }

        public List<string> GetAllNicknames() => _nicknames;

        public void AddNickname(string nickname)
        {
            _nicknames.Add(nickname);
        }

        public void RemoveNickname(string nickname)
        {
            _nicknames.Remove(nickname);
        }
    }
}
