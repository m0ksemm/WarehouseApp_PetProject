using ServiceContracts.DTOs.ManufacturersDTOs;
using ServiceContracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ManufacturersService : IManufacturersService
    {
        private readonly HttpClient _httpClient;

        public ManufacturersService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> AddManufacturer(ManufacturerAddRequest manufacturerAddRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7053/Manufacturers/CreateManufacturer", manufacturerAddRequest);

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

        public async Task<bool> DeleteManufacturer(Guid guid)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7053/Manufacturers/DeleteManufacturer/{guid}");

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

        public async Task<List<ManufacturerResponse>> GetAllManufacturers()
        {
            var manufacturers = await _httpClient.GetFromJsonAsync<List<ManufacturerResponse>>("https://localhost:7053/Manufacturers/GetAllManufacturers");
            if (manufacturers != null)
            {
                return manufacturers;
            }
            else throw new Exception("There are no manufacturers.");
        }

        public Task<ManufacturerResponse?> GetManufacturerById(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<ManufacturerResponse?> GetManufacturerByName(string manufacturerName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateManufacturer(ManufacturerUpdateRequest manufacturerUpdateRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7053/Manufacturers/UpdateManufacturer/{manufacturerUpdateRequest.ManufacturerID}", manufacturerUpdateRequest);

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
