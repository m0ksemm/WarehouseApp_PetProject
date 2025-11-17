using ServiceContracts.DTOs.WarehousesDTOs;
using ServiceContracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WarehousesService : IWarehousesService
    {
        private readonly HttpClient _httpClient;

        public WarehousesService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> AddWarehouse(WarehouseAddRequest warehouseAddRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7053/Warehouses/CreateWarehouse", warehouseAddRequest);

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

        public async Task<bool> DeleteWarehouse(Guid guid)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7053/Warehouses/DeleteWarehouse/{guid}");

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

        public async Task<List<WarehouseResponse>> GetAllWarehouses()
        {
            var warehouse = await _httpClient.GetFromJsonAsync<List<WarehouseResponse>>("https://localhost:7053/Warehouses/GetAllWarehouses");
            if (warehouse != null)
            {
                return warehouse;
            }
            else throw new Exception("There are no warehouses.");
        }

        public Task<WarehouseResponse?> GetWarehouseById(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<WarehouseResponse?> GetWarehouseByName(string warehouseName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateWarehouse(WarehouseUpdateRequest warehouseUpdateRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7053/Warehouses/UpdateWarehouse/{warehouseUpdateRequest.WarehouseID}", warehouseUpdateRequest);

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

