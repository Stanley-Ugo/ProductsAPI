using ProductsAPI.DomainLayer;
using ProductsAPI.DomainLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.DataAccessLayerEFCore.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public decimal SumOfProductsInAweek()
        {
            decimal productSum = 0;

            var result = _context.Products.Where(x => x.DateCreated.Date > DateTime.Now.AddDays(-7));

            if (result != null)
            {
                foreach (var product in result)
                {
                    productSum += product.Price;
                }
            }

            return productSum;
        }

        public IEnumerable<Product> GetAllDisabledProductsRepo()
        {
            var result = _context.Products.Where(x => x.isActive == false).OrderByDescending(x => x.DateCreated);

            return result;
        }

        public IEnumerable<Product> GetAllNonDisabledProductRepo()
        {
            var result = _context.Products.Where(x => x.isActive == true);

            return result;
        }
    }
}
