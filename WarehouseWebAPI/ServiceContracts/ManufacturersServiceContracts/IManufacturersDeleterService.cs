using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ManufacturersServiceContracts
{
    public interface IManufacturersDeleterService
    {
        Task<bool> DeleteManufacturer(Guid? manufacturerID);
    }
}
