using ServiceContracts.DTOs.ProductsDTOs;
using ServiceContracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductsService : IProductsService
    {
        private readonly HttpClient _httpClient;

        public ProductsService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> AddProduct(ProductAddRequest productAddRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7053/Products/CreateProduct", productAddRequest);

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


        public async Task<bool> DeleteProduct(Guid guid)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7053/Products/DeleteProduct/{guid}");

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

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            var products = await _httpClient.GetFromJsonAsync<List<ProductResponse>>("https://localhost:7053/Products/GetAllProducts");
            if (products != null)
            {
                return products;
            }
            else throw new Exception("There are no products.");
        }


        public Task<ProductResponse?> GetProductByName(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse?> GetProductById(Guid guid)
        {
            throw new NotImplementedException();
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
