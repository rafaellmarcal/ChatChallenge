using ChatChallenge.Resources.Constants;
using ChatChallenge.ServerSide.Services.Interfaces;
using System.Linq;

namespace ChatChallenge.ServerSide.Services
{
    public class NicknameValidateService : INicknameValidateService
    {
        private readonly TcpClientHandler _tcpClientHandler;

        public NicknameValidateService(TcpClientHandler tcpClientHandler)
        {
            _tcpClientHandler = tcpClientHandler;
        }

        public string ValidateNickname(string message)
        {
            string response;

            if (_tcpClientHandler.GetAllNicknames().Any(n => n == message))
                response = $"false{ChatChallengeConstants.MESSAGE_LIMITER}Sorry, the nickname {message} is already taken. Please choose a different one:";
            else
            {
                _tcpClientHandler.AddNickname(message);
                response = $"true{ChatChallengeConstants.MESSAGE_LIMITER}You are registered as {message}. Joining #general.";
            }

            return response;
        }
    }
}
