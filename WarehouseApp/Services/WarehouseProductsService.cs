using Entities;
using ServiceContracts.DTOs.ProductsDTOs;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
using ServiceContracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WarehouseProductsService : IWarehouseProductsService
    {
        private readonly HttpClient _httpClient;

        public WarehouseProductsService()
        {
            _httpClient = new HttpClient();
        }



        public async Task<bool> AddWarehouseProduct(WarehouseProductAddRequest warehouseProductAddRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7053/WarehouseProducts/CreateWarehouseProduct", warehouseProductAddRequest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = $"Error: \n{response.StatusCode}";

                throw new Exception(errorMessage);
            }
        }

        public async Task<List<WarehouseProductResponse>> GetAllWarehouseProducts()
        {
            var warehouseProducts = await _httpClient.GetFromJsonAsync<List<WarehouseProductResponse>>("https://localhost:7053/Products/GetAllProducts");
            if (warehouseProducts != null)
            {
                return warehouseProducts;
            }
            else throw new Exception("There are no products.");
        }

        public Task<WarehouseProductResponse?> GetWarehouseProductByWarehouseProductId(Guid warehouseProductId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WarehouseProductResponse>?> GetWarehouseProductsByWarehouseId(Guid WarehouseId)
        {
            var warehouseProducts = await _httpClient.GetFromJsonAsync<List<WarehouseProductResponse>>($"https://localhost:7053/WarehouseProducts/GetWarehouseProductsByWarehouseId/{WarehouseId}");
            if (warehouseProducts != null)
            {
                return warehouseProducts;
            }
            else throw new Exception("There are no products.");
        }

        public async Task<bool> UpdateWarehouseProduct(WarehouseProductUpdateRequest warehouseProductUpdateRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7053/Products/UpdateProduct/{warehouseProductUpdateRequest.WarehouseProductID}", warehouseProductUpdateRequest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = $"Error: \n{response.StatusCode}";

                throw new Exception(errorMessage);
            }
        }

        public async Task<bool> DeleteWarehouseProduct(Guid warehouseProductID)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7053/WarehouseProducts/DeleteWarehouseProduct/{warehouseProductID}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = $"Error: \n{response.StatusCode}";

                throw new Exception(errorMessage);
            }
        }









        public async Task<bool> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7053/Products/UpdateProduct/{productUpdateRequest.ProductID}", productUpdateRequest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = $"Error: \n{response.StatusCode}";

                throw new Exception(errorMessage);
            }
        }

    }
}
