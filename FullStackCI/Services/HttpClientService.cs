using System.Text.Json;
using FullStackCI.Dtos;

namespace FullStackCI.Services
{
    public class HttpClientService(IHttpClientFactory httpClientFactory)
    {
        private readonly IHttpClientFactory _httpClient = httpClientFactory;

        public async Task<RespuestaHaciendaDto> GetHaciendaResponse(string cedula)
        {
            try
            {
                var _client = _httpClient.CreateClient();

                var _response = await _client.GetAsync($"https://api.hacienda.go.cr/fe/ae?identificacion={cedula}");

                _response.EnsureSuccessStatusCode();

                var json = await _response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<RespuestaHaciendaDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
