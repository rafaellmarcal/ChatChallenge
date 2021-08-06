using ChatChallenge.ClientSide.Models;
using System.Net.Sockets;

namespace ChatChallenge.ClientSide.Services.Interfaces
{
    public interface INicknameValidateService
    {
        void ValidateNickname(Client client, NetworkStream stream);
    }
}
