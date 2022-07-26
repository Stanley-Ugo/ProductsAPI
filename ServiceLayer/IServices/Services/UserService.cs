using ProductsAPI.DomainLayer;
using ProductsAPI.DomainLayer.IRepository;
using ProductsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.ServiceLayer.IServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<UserViewModel> GetAllUsersService()
        {
            List<UserViewModel> userViewModel = new List<UserViewModel>();

            var result = _unitOfWork.User.GetAll();

            if (result != null)
            {
                foreach (var user in result)
                {
                    userViewModel.Add(new UserViewModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        Role = user.Role
                    });
                }
            }

            return userViewModel;
        }

        public bool CreateUserService(UserViewModel userVM)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userVM.Name) && !string.IsNullOrEmpty(userVM.Email) && !string.IsNullOrEmpty(userVM.Password) && !string.IsNullOrEmpty(userVM.Role))
            {
                User user = new User
                {
                    Name = userVM.Name,
                    Email = userVM.Email,
                    Password = userVM.Password,
                    Role = userVM.Role
                };

                _unitOfWork.User.Add(user);
                var response = _unitOfWork.Save();

                if (response > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public UserViewModel GetUserByEmailAndPassword(LoginViewModel loginViewModel)
        {
            UserViewModel userViewModel = new UserViewModel();

            var result = _unitOfWork.User.Find(x => x.Email == loginViewModel.Email && x.Password == loginViewModel.Password).FirstOrDefault();

            if (result != null)
            {
                userViewModel.Id = result.Id;
                userViewModel.Name = result.Name;
                userViewModel.Email = result.Email;
                userViewModel.Password = result.Password;
                userViewModel.Role = result.Role;

            }

            return userViewModel;
        }
    }
}
