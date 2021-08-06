namespace ChatChallenge.ServerSide.Services.Interfaces
{
    public interface IServerActionHandler
    {
        void Handle(object tcpClient);
    }
}
