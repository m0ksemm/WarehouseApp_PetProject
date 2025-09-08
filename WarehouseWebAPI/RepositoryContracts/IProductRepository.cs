using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid guid);
        Task<Product?> GetProductByName(string productName);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(Guid guid);
    }
}
