using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.ProductsDTO;
using ServiceContracts.ProductsServiceContracts;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductsServices
{
    public class ProductsUpdaterService : IProductsUpdaterService
    {
        private readonly IProductRepository _productRepository;
        public ProductsUpdaterService(IProductRepository productsRepository)
        {
            _productRepository = productsRepository;
        }
        public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest? productUpdateRequest)
        {
            if (productUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(productUpdateRequest));
            }
            ValidationHelper.ModelValidation(productUpdateRequest);

            List<Product> products = await _productRepository.GetAllProducts();
            if (products.Select(product => product.ProductName).Contains(productUpdateRequest.ProductName))
            {
                throw new ArgumentException("Product with this name already exists.");
            }

            Product? matchingProduct = await _productRepository
                .GetProductById(productUpdateRequest.ProductID);
            if (matchingProduct == null)
            {
                throw new ArgumentException("Given Product ID does not exist.");
            }
            matchingProduct.ProductName = productUpdateRequest.ProductName;
            matchingProduct.CategoryID = productUpdateRequest.CategoryID;
            matchingProduct.ManufacturerID = productUpdateRequest.ManufacturerID;
            matchingProduct.Weight = productUpdateRequest.Weight;
            matchingProduct.Price = productUpdateRequest.Price;
            matchingProduct.BarCode = productUpdateRequest.BarCode;

            await _productRepository.UpdateProduct(matchingProduct);
            return matchingProduct.ToProductResponse();
        }
    }
}
