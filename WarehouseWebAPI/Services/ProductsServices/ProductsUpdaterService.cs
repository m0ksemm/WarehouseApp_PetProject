using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.ProductsDTO;
using ServiceContracts.ProductsServiceContracts;
using ServiceContracts.CategoriesServiceContracts;

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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public ProductsUpdaterService(IProductRepository productsRepository, ICategoryRepository categoriesRepository, IManufacturerRepository manufacturerRepository)
        {
            _productRepository = productsRepository;
            _categoryRepository = categoriesRepository;
            _manufacturerRepository = manufacturerRepository;
        }
        public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest? productUpdateRequest)
        {
            if (productUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(productUpdateRequest));
            }
            ValidationHelper.ModelValidation(productUpdateRequest);

            List<Product> products = await _productRepository.GetAllProducts();
            if (products.Any(p => productUpdateRequest.ProductName == p.ProductName &&
                   productUpdateRequest.ManufacturerID == p.ManufacturerID &&
                   productUpdateRequest.CategoryID == p.CategoryID &&
                   productUpdateRequest.BarCode == p.BarCode &&
                   productUpdateRequest.Price == p.Price &&
                   productUpdateRequest.Weight == p.Weight))
            {
                throw new ArgumentException("Such product already exists.");
            }

            Product? matchingProduct = await _productRepository
                .GetProductById(productUpdateRequest.ProductID);
            if (matchingProduct == null)
            {
                throw new ArgumentException("Given Product ID does not exist.");
            }
            if (matchingProduct.ProductName != productUpdateRequest.ProductName) 
            {
                matchingProduct.ProductName = productUpdateRequest.ProductName;
            }
            if (matchingProduct.CategoryID != productUpdateRequest.CategoryID) 
            {
                matchingProduct.CategoryID = productUpdateRequest.CategoryID;
                Category? updatedCategory = await _categoryRepository.GetCategoryById(productUpdateRequest.CategoryID!.Value);
                if (updatedCategory == null)
                {
                    throw new ArgumentException("Given for update Category does not exist.");
                }
                matchingProduct.Category = new Category
                {
                    CategoryID = updatedCategory.CategoryID,
                    CategoryName = updatedCategory.CategoryName
                };
            }

            if (matchingProduct.ManufacturerID != productUpdateRequest.ManufacturerID)
            {
                matchingProduct.ManufacturerID = productUpdateRequest.ManufacturerID;
                Manufacturer? updatedManufacturer = await _manufacturerRepository.GetManufacturerById(productUpdateRequest.ManufacturerID!.Value);
                if (updatedManufacturer == null)
                {
                    throw new ArgumentException("Given for update Manufacturer does not exist.");
                }
                matchingProduct.Manufacturer = new Manufacturer
                {
                    ManufacturerID = updatedManufacturer.ManufacturerID,
                    ManufacturerName = updatedManufacturer.ManufacturerName
                };
            }
            if (matchingProduct.Weight != productUpdateRequest.Weight)
            {
                matchingProduct.Weight = productUpdateRequest.Weight;
            }
            if (matchingProduct.Price != productUpdateRequest.Price)
            {
                matchingProduct.Price = productUpdateRequest.Price;
            }
            if (matchingProduct.BarCode != productUpdateRequest.BarCode)
            {
                matchingProduct.BarCode = productUpdateRequest.BarCode;
            }

            await _productRepository.UpdateProduct(matchingProduct);
            return matchingProduct.ToProductResponse();
        }
    }
}
