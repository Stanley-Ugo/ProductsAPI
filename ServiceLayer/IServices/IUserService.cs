using ProductsAPI.ViewModels;
using System.Collections.Generic;

namespace ProductsAPI.ServiceLayer.IServices
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAllUsersService();

        bool CreateUserService(UserViewModel user);

        UserViewModel GetUserByEmailAndPassword(UserViewModel userViewModel);

    }
}
