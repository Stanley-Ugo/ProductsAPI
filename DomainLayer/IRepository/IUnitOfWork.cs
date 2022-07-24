using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.DomainLayer.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }

        IUserRepository  User { get; }

        int Save();
    }
}
