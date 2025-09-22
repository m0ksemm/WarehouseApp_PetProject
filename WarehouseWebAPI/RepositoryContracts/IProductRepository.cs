using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid guid);
        Task<List<Product>> GetFilteredProducts(Expression<Func<Product, bool>> predicate);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid guid);
    }
}
