namespace ChatChallenge.ClientSide.Services.Interfaces
{
    public interface IServerMessageReceiver
    {
        void WriteMessageReceived(object tcpClientObject);
    }
}
