using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.DomainLayer.IRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        decimal SumOfProductsInAweek();
        IEnumerable<Product> GetAllDisabledProductsRepo();
        IEnumerable<Product> GetAllNonDisabledProductRepo();
    }
}
