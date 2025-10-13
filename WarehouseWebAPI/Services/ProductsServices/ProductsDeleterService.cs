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
        private readonly IWarehouseProductRepository _warehouseProductRepository;

        public ProductsDeleterService(IProductRepository productsRepository
            , IWarehouseProductRepository warehouseProductRepository)
        {
            _productRepository = productsRepository;
            _warehouseProductRepository = warehouseProductRepository;
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

            List<WarehouseProduct> warehouseProducts = await _warehouseProductRepository.GetAllWarehouseProducts();
            if (warehouseProducts.Any(warehouseProduct => warehouseProduct.ProductID == productID)) 
            {
                throw new ArgumentException("This Product can not be deleted since it exists in Warehouses.");
            }

            await _productRepository.DeleteProduct(productID.Value);
            return true;
        }
    }
}
