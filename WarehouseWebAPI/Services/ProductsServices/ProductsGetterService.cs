using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.ProductsDTO;
using ServiceContracts.ProductsServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductsServices
{
    public class ProductsGetterService : IProductsGetterService
    {
        private readonly IProductRepository _productsRepository;
        public ProductsGetterService(IProductRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            var products = await _productsRepository.GetAllProducts();

            return products
              .Select(temp => temp.ToProductResponse()).ToList();
        }

        public async Task<ProductResponse?> GetProductById(Guid? guid)
        {
            if (guid == null)
                return null;

            Product? product = await _productsRepository.GetProductById(guid.Value);

            if (product == null)
                return null;

            return product.ToProductResponse();
        }
    }
}
