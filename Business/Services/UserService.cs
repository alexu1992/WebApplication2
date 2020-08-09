using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Model.Request;
using Model.Response;
using System;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserResponse Get(UserRequest userRequest)
        {
            if (userRequest?.Username == null)
            {
                return null;
            }
            var user = _userRepository.Get(userRequest.Username);

            if (user == null)
            {
                return _userRepository.Create(userRequest.Username);
            }
            else if (user.EndDate > DateTime.Now)
            {
                return user;
            }
            else
            {
                return _userRepository.UpdatePassword(userRequest.Username);
            }
        }
    }
}
