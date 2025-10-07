using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RepositoryContracts;
using ServiceContracts.DTOs.ProductsDTO;
using ServiceContracts.ProductsServiceContracts;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductsServices
{
    public class ProductsAdderService : IProductsAdderService
    {
        private readonly IProductRepository _productsRepository;
        private readonly ICategoryRepository _categpriesRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public ProductsAdderService(IProductRepository productsRepository, 
            ICategoryRepository categoryRepository,
            IManufacturerRepository manufacturerRepository)
        {
            _productsRepository = productsRepository;
            _categpriesRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<ProductResponse> AddProduct(ProductAddRequest? productAddRequest)
        {
            if (productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }

            List<Product> products = await _productsRepository.GetAllProducts();
            if (products.Any(product => product.ProductName == productAddRequest.ProductName &&
                        product.ManufacturerID == productAddRequest.ManufacturerID && 
                        product.CategoryID == productAddRequest.CategoryID && 
                        product.Weight == productAddRequest.Weight && 
                        product.Price == productAddRequest.Price &&
                        product.BarCode == productAddRequest.BarCode))
            {
                throw new ArgumentException("Such product already exists.");
            }

            List<Category> categories = await _categpriesRepository.GetAllCategories();
            if (categories.FirstOrDefault(category => category.CategoryID == productAddRequest.CategoryID) == null) 
            {
                throw new ArgumentException("Such Category does not exist.");
            }

            List<Manufacturer> manufacturers = await _manufacturerRepository.GetAllManufacturers();
            if (manufacturers.FirstOrDefault(manufacturer => manufacturer.ManufacturerID == productAddRequest.ManufacturerID) == null)
            {
                throw new ArgumentException("Such Manufacturer does not exist.");
            }

            ValidationHelper.ModelValidation(productAddRequest);

            Product product = productAddRequest.ToProduct();
            product.ProductID = Guid.NewGuid();

            await _productsRepository.AddProduct(product);

            return product.ToProductResponse();
        }
    }
}
