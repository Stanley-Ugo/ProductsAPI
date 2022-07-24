using ProductsAPI.ViewModels;
using System.Collections.Generic;

namespace ProductsAPI.ServiceLayer.IServices
{
    public interface IProductService
    {
        IEnumerable<ProductViewModel> GetAllProductService();
        ProductViewModel GetProductByIdService(int id);
        bool CreateProductService(ProductViewModel productVM);
        bool UpdateProductService(ProductViewModel productVM);
        ProductViewModel DeleteProductByIdService(int id);
        ProductViewModel DisableProductByIdService(int id);
        decimal sumOfProductsInAWeekService();
        IEnumerable<ProductViewModel> GetAllDisabledProductService();
        IEnumerable<ProductViewModel> GetAllNonDisabledProductService();
    }
}
