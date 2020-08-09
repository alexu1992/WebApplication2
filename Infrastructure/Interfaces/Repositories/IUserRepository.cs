using Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository
    {
        UserResponse Get(string username);

        UserResponse Create(string username);

        UserResponse UpdatePassword(string username);
    }
}
