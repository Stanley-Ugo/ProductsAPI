using ProductsAPI.DomainLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.DataAccessLayerEFCore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Product = new ProductRepository(this.context);
            User = new UserRepository(this.context);
        }
        public IProductRepository Product
        {
            get;
            private set;
        }
        public IUserRepository User
        {
            get;
            private set;
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
