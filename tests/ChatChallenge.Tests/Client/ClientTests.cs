using ChatChallenge.ClientSide.Services;
using Xunit;

namespace ChatChallenge.Tests
{
    public class ClientTests
    {
        private readonly NicknameValidateService _nicknameValidateService;

        public ClientTests()
        {
            _nicknameValidateService = new NicknameValidateService();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
