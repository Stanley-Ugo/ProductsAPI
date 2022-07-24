using ProductsAPI.DomainLayer;
using ProductsAPI.DomainLayer.IRepository;
using ProductsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.ServiceLayer.IServices.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ProductViewModel> GetAllProductService()
        {
            List<ProductViewModel> productViewModel = new List<ProductViewModel>();

            var result = _unitOfWork.Product.GetAll();

            if (result != null)
            {
                foreach (var product in result)
                {
                    productViewModel.Add(new ProductViewModel 
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        DateCreated = product.DateCreated,
                        isActive = product.isActive
                    });
                }
            }

            return productViewModel;
        }

        public ProductViewModel GetProductByIdService(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel();

            var productInDb = _unitOfWork.Product.GetById(id);

            if (productInDb != null )
            {
                productViewModel.Id = productInDb.Id;
                productViewModel.Name = productInDb.Name;
                productViewModel.Price = productInDb.Price;
                productViewModel.DateCreated = productInDb.DateCreated;
                productViewModel.isActive = productInDb.isActive;
            }

            return productViewModel;
        }

        public bool CreateProductService(ProductViewModel productVM)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(productVM.Name))
            {
                Product product = new Product
                {
                    Name = productVM.Name,
                    Price = productVM.Price,
                    DateCreated = DateTime.Now,
                    isActive = productVM.isActive,
                };

                _unitOfWork.Product.Add(product);
                var response = _unitOfWork.Save();

                if (response > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool UpdateProductService(ProductViewModel productVM)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(productVM.Name))
            {
                var product = _unitOfWork.Product.GetById(productVM.Id);

                product.Name = productVM.Name;
                product.Price = productVM.Price;
                product.DateCreated = productVM.DateCreated;
                product.isActive = productVM.isActive;


                _unitOfWork.Product.Update(product);
                var response = _unitOfWork.Save();

                if (response > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public ProductViewModel DisableProductByIdService(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel();

            var productInDb = _unitOfWork.Product.GetById(id);

            if (productInDb != null)
            {
                productViewModel.Id = productInDb.Id;
                productViewModel.Name = productInDb.Name;
                productViewModel.Price = productInDb.Price;
                productViewModel.DateCreated = productInDb.DateCreated;
                productViewModel.isActive = productInDb.isActive = false;

                _unitOfWork.Product.Update(productInDb);
                _unitOfWork.Save();

                return productViewModel;
            }

            return productViewModel;
        }

        public ProductViewModel DeleteProductByIdService(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel();

            var productInDb = _unitOfWork.Product.GetById(id);

            if (productInDb != null)
            {
                productViewModel.Id = productInDb.Id;
                productViewModel.Name = productInDb.Name;
                productViewModel.Price = productInDb.Price;
                productViewModel.DateCreated = productInDb.DateCreated;
                productViewModel.isActive = productInDb.isActive;

                _unitOfWork.Product.Remove(productInDb);
                _unitOfWork.Save();

                return productViewModel;
            }

            return productViewModel;
        }

        public decimal sumOfProductsInAWeekService()
        {
            return _unitOfWork.Product.SumOfProductsInAweek();
        }

        public IEnumerable<ProductViewModel> GetAllDisabledProductService()
        {
            List<ProductViewModel> productViewModel = new List<ProductViewModel>();

            var result = _unitOfWork.Product.GetAllDisabledProductsRepo();

            if (result != null)
            {
                foreach (var product in result)
                {
                    productViewModel.Add(new ProductViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        DateCreated = product.DateCreated,
                        isActive = product.isActive
                    });
                }
            }

            return productViewModel;
        }

        public IEnumerable<ProductViewModel> GetAllNonDisabledProductService()
        {
            List<ProductViewModel> productViewModel = new List<ProductViewModel>();

            var result = _unitOfWork.Product.GetAllNonDisabledProductRepo();

            if (result != null)
            {
                foreach (var product in result)
                {
                    productViewModel.Add(new ProductViewModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        DateCreated = product.DateCreated,
                        isActive = product.isActive
                    });
                }
            }

            return productViewModel;
        }
    }
}
