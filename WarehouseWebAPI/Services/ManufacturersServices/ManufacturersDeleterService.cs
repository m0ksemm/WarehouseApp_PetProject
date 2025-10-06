using Entities;
using RepositoryContracts;
using ServiceContracts.ManufacturersServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ManufacturersService
{
    public class ManufacturersDeleterService : IManufacturersDeleterService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IProductRepository _productRepository;

        public ManufacturersDeleterService(IManufacturerRepository manufacturerRepository, IProductRepository productRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> DeleteManufacturer(Guid? manufacturerID)
        {
            if (manufacturerID == null || manufacturerID == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(manufacturerID));
            }
            Manufacturer? manufacturer = await _manufacturerRepository.GetManufacturerById(manufacturerID.Value);
            if (manufacturer == null)
            {
                return false;
            }
            List<Product> products = await _productRepository.GetAllProducts();
            if (products.Select(product => product.ManufacturerID == manufacturerID).Count() != 0)
            {
                throw new ArgumentException("This manufacturer can not be deleted since there are products that belong to it.");
            }
            await _manufacturerRepository.DeleteManufacturer(manufacturerID.Value);
            return true;
        }
    }
}
