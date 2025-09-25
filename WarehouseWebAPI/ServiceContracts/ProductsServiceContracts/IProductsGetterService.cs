using ServiceContracts.DTOs.ManufacturersDTO;
using ServiceContracts.DTOs.ProductsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ProductsServiceContracts
{
    public interface IProductsGetterService
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse?> GetProductById(Guid? guid);
    }
}
