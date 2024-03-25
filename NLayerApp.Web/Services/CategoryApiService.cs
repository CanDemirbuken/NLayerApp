﻿using NLayerApp.Core.DTOs;

namespace NLayerApp.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;
        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetAlAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("categories");
            return response.Data;
        }
    }
}