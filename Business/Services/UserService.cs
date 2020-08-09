using Infrastructure.Interfaces;
using Model.Request;
using Model.Response;
using System;

namespace Business.Services
{
    public class UserService : IUserService
    {
        public UserResponse Get(UserRequest userRequest)
        {
            if (userRequest?.Username == null)
            {
                return null;
            }

            return new UserResponse
            {
                Username = userRequest.Username,
                Password = Guid.NewGuid().ToString(),
                EndDate = DateTime.Now.AddSeconds(30)
            };
        }
    }
}
