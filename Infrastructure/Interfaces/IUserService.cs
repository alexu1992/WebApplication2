using Model.Request;
using Model.Response;

namespace Infrastructure.Interfaces
{
    public interface IUserService
    {
        UserResponse Get(UserRequest userRequest);
    }
}
