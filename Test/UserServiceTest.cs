using Business.Services;
using Database.Context;
using Database.DAL;
using Database.DB;
using Infrastructure.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Model.Request;
using Model.Response;
using Moq;
using System;
using Xunit;

namespace Test
{
    public class UserServiceTest
    {

        [Theory]
        [InlineData("alex")]
        [InlineData("radu")]
        [InlineData("ion")]
        [InlineData("")]
        public void Get_WhenUserNotExist_ShouldCreateIt(string request)
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object);

            mockUserRepository.Setup(x => x.Get(request))
                 .Returns((UserResponse)null);

            mockUserRepository.Setup(x => x.Create(request))
                  .Returns(new UserResponse()
                  {
                      Username = request
                  });

            //act
            var user = userService.Get(new UserRequest() { Username = request });

            //assert
            Assert.Equal(request, user.Username);
            mockUserRepository.Verify(x => x.Get(request), Times.Once());
            mockUserRepository.Verify(x => x.Create(request), Times.Once());
            mockUserRepository.Verify(x => x.UpdatePassword(request), Times.Never());
        }


        [Theory]
        [InlineData("alex")]
        [InlineData("radu")]
        [InlineData("ion")]
        [InlineData("")]
        public void Get_WhenUserExistAndPasswordIsExpired_ShouldUpdateIt(string request)
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object);
            var actualUser = new UserResponse()
            {
                Username = request,
                Password = Guid.NewGuid().ToString(),
                EndDate = DateTime.Now.AddMinutes(-30)
            };

            mockUserRepository.Setup(x => x.Get(request))
                 .Returns(actualUser);

            mockUserRepository.Setup(x => x.UpdatePassword(request))
                .Returns(new UserResponse() { Username = request, Password = Guid.NewGuid().ToString() });

            //act
            var user = userService.Get(new UserRequest() { Username = request });

            //assert
            Assert.Equal(request, user.Username);
            Assert.NotEqual(actualUser.Password, user.Password);
            mockUserRepository.Verify(x => x.Get(request), Times.Once());
            mockUserRepository.Verify(x => x.Create(request), Times.Never());
            mockUserRepository.Verify(x => x.UpdatePassword(request), Times.Once());
        }


        [Theory]
        [InlineData("alex")]
        [InlineData("radu")]
        [InlineData("ion")]
        [InlineData("")]
        public void Get_WhenUserExistAndPasswordIsNotExpired_ShouldReturnIt(string request)
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object);
            var actualUser = new UserResponse()
            {
                Username = request,
                Password = Guid.NewGuid().ToString(),
                EndDate = DateTime.Now.AddMinutes(30)
            };

            mockUserRepository.Setup(x => x.Get(request))
                 .Returns(actualUser);


            //act
            var user = userService.Get(new UserRequest() { Username = request });

            //assert
            Assert.Equal(request, user.Username);
            Assert.Equal(actualUser.Password, user.Password);
            mockUserRepository.Verify(x => x.Get(request), Times.Once());
            mockUserRepository.Verify(x => x.Create(request), Times.Never());
            mockUserRepository.Verify(x => x.UpdatePassword(request), Times.Never());
        }

        [Theory]
        [InlineData(null)]
        public void Get_WhenRequestIsNull_ShouldReturnNull(string request)
        {
            //arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object);
            var actualUser = (UserResponse)null;

            mockUserRepository.Setup(x => x.Get(request))
                 .Returns(actualUser);


            //act
            var user = userService.Get(new UserRequest() { Username = request });

            //assert
            Assert.Null(user);
            mockUserRepository.Verify(x => x.Get(request), Times.Never());
            mockUserRepository.Verify(x => x.Create(request), Times.Never());
            mockUserRepository.Verify(x => x.UpdatePassword(request), Times.Never());
        }
    }
}
