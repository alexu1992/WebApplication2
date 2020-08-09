using Business.Services;
using Model.Request;
using System;
using Xunit;

namespace Test
{
    public class UserTest
    {
        private readonly UserService _userService;

        public UserTest()
        {
            _userService = new UserService();
        }


        [Fact]
        public void StringEmptyRequest()
        {
            var result = _userService.Get(new UserRequest() { Username = string.Empty });

            Assert.Equal(result.Username, string.Empty);
        }


        [Fact]
        public void NullRequest()
        {
            var result = _userService.Get(null);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("alex")]
        [InlineData("radu")]
        [InlineData("ion")]
        public void AnyRequest(string request)
        {
            var result = _userService.Get(new UserRequest() { Username = request });

            Assert.NotNull(result);
        }

    }
}
