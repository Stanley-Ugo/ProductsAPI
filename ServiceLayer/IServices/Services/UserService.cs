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
    }
}
