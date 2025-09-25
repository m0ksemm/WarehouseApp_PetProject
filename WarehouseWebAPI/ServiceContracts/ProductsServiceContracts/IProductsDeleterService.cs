using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ProductsServiceContracts
{
    internal interface IProductsDeleterService
    {
        Task<bool> DeleteProduct(Guid? productID);
    }
}
