using ChatChallenge.ClientSide.Models;
using System.Net.Sockets;

namespace ChatChallenge.ClientSide.Services.Interfaces
{
    public interface ISendingMessageService
    {
        void Send(Client client, NetworkStream stream);
    }
}
