using ProductsAPI.DomainLayer;
using ProductsAPI.DomainLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.DataAccessLayerEFCore.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
