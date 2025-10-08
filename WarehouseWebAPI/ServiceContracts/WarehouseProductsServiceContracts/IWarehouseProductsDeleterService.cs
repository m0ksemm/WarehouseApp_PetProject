using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.WarehouseProductsServiceContracts
{
    public interface IWarehouseProductsDeleterService
    {
        Task<bool> DeleteWarehouseProduct(Guid? warehouseProductID);
    }
}
