namespace ChatChallenge.ClientSide.Models
{
    public class Client
    {
        public string Nickname { get; private set; }
        public string IpAddress { get; private set; }
        public int Port { get; private set; }

        public Client(string nickname, string ipAddress, int port)
        {
            Nickname = nickname;
            IpAddress = ipAddress;
            Port = port;
        }

        public void UpdateUsername(string nickname)
        {
            Nickname = nickname;
        }
    }
}
