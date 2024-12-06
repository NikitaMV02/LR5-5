using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ApiClientExample
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://api.wordnik.com/v4";

        private readonly string _apiKey = "jvb7mxg557mzrn2a181vgxpg7ueteuhkqc292oox4ad1eqdib";

        public ApiClient(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }


        public async Task<ApiResponse<List<WordObject>>> GetAsync(string endpoint)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
                var response = await _httpClient.GetAsync($"{ApiBaseUrl}/{endpoint}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<WordObject>>();
                    return new ApiResponse<List<WordObject>>
                    {
                        Message = "Request successful",
                        HttpStatusCode = StatusCodes.Status200OK,
                        Data = data
                    };
                }
                else
                {
                    return new ApiResponse<List<WordObject>>
                    {
                        Message = $"Error: {response.ReasonPhrase}",
                        HttpStatusCode = (int)response.StatusCode,
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<WordObject>>
                {
                    Message = $"Error occurred: {ex.Message}",
                    HttpStatusCode = StatusCodes.Status500InternalServerError,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<List<WordObject>>> PostAsync(string endpoint, object payload)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
                var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/{endpoint}", payload);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<WordObject>>();
                    return new ApiResponse<List<WordObject>>
                    {
                        Message = "Request successful",
                        HttpStatusCode = StatusCodes.Status200OK,
                        Data = data
                    };
                }
                else
                {
                    return new ApiResponse<List<WordObject>>
                    {
                        Message = $"Error: {response.ReasonPhrase}",
                        HttpStatusCode = (int)response.StatusCode,
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<WordObject>>
                {
                    Message = $"Error occurred: {ex.Message}",
                    HttpStatusCode = StatusCodes.Status500InternalServerError,
                    Data = null
                };
            }
        }

    }
}
