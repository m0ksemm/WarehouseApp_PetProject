using ServiceContracts.DTOs.ProductsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ServiceContracts
{
    public interface IProductsService
    {
        Task<bool> AddProduct(ProductAddRequest product);
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse?> GetProductById(Guid guid);
        Task<ProductResponse?> GetProductByName(string productName);
        Task<bool> UpdateProduct(ProductUpdateRequest product);
        Task<bool> DeleteProduct(Guid guid);
    }
}
