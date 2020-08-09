using Model.Request;
using Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.Services
{
    public interface IUserService
    {
        UserResponse Get(UserRequest userRequest);
    }
}
