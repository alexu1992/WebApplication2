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
    public class UserTest
    {

        [Theory]
        [InlineData("alex")]
        [InlineData("radu")]
        [InlineData("ion")]
        [InlineData("")]
        public void AnyRequestCreate(string request)
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.Create(request))
                .Returns(new UserResponse()
                {
                    Username = request
                });
            mockUserRepository.Object.Create(request);
            mockUserRepository.Verify(x => x.Create(request), Times.Once());
        }


        [Theory]
        [InlineData("alex")]
        [InlineData("radu")]
        [InlineData("ion")]
        [InlineData("")]
        public void AnyRequestUpdate(string request)
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.UpdatePassword(request))
                .Returns(new UserResponse()
                {
                    Username = request
                });
            mockUserRepository.Object.UpdatePassword(request);
            mockUserRepository.Verify(x => x.UpdatePassword(request), Times.Once());
        }


        [Theory]
        [InlineData("alex")]
        [InlineData("radu")]
        [InlineData("ion")]
        [InlineData("")]
        public void AnyRequesGet(string request)
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.Get(request))
                .Returns(new UserResponse()
                {
                    Username = request
                });
            Assert.Equal(request, mockUserRepository.Object.Get(request).Username);
            mockUserRepository.Verify(x => x.Get(request), Times.Once());
        }
    }
}
