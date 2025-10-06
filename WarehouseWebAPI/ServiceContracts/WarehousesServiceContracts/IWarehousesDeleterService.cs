using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.WarehousesServiceContracts
{
    public interface IWarehousesDeleterService
    {
        Task<bool> DeleteWarehouse(Guid? warehouseID);
    }
}
