using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.CategoriesServiceContracts
{
    public interface ICategoriesDeleterService
    {
        Task<bool> DeleteCategory(Guid? categoryID);
    }
}
