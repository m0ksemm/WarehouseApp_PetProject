using Entities;
using RepositoryContracts;
using ServiceContracts.ProductsServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductsServices
{
    public class ProductsDeleterService : IProductsDeleterService
    {
        private readonly IProductRepository _productRepository;
        public ProductsDeleterService(IProductRepository productsRepository)
        {
            _productRepository = productsRepository;
        }
        public async Task<bool> DeleteProduct(Guid? productID)
        {
            if (productID == null || productID == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productID));
            }
            Product? product = await _productRepository.GetProductById(productID.Value);
            if (product == null)
            {
                return false;
            }

            //List<Product> products = 
            //Chech if products with such category exist

            await _productRepository.DeleteProduct(productID.Value);
            return true;
        }
    }
}
