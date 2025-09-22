using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteProduct(Guid guid)
        {
            _dbContext.Products.RemoveRange(_dbContext.Products.Where(temp => temp.ProductID == guid));
            int rowsDeleted = await _dbContext.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _dbContext.Products.Include("Category").Include("Manufacturer").ToListAsync();
        }


        public async Task<List<Product>> GetFilteredProducts(Expression<Func<Product, bool>> predicate)
        {
            return await _dbContext.Products.Include("Category").Include("Manufacturer")
                .Where(predicate).ToListAsync();
        }

        public async Task<Product?> GetProductById(Guid guid)
        {
            return await _dbContext.Products.Include("Category").Include("Manufacturer")
                .FirstOrDefaultAsync(temp => temp.ProductID == guid);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            Product? matchingProduct = await _dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductID == product.ProductID);
            if (matchingProduct == null)
            {
                return product;
            }
            matchingProduct.ProductName = product.ProductName;
            matchingProduct.Weight = product.Weight;
            matchingProduct.Price = product.Price;
            matchingProduct.BarCode = product.BarCode;
            matchingProduct.CategoryID = product.CategoryID;
            matchingProduct.ManufacturerID = product.ManufacturerID;

            int countUpdated = await _dbContext.SaveChangesAsync();

            return matchingProduct;
        }
    }
}
