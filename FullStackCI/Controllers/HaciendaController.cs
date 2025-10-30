using FullStackCI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullStackCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaciendaController(HttpClientService httpClientService) : ControllerBase
    {
        private readonly HttpClientService _httpClientService = httpClientService;
        [HttpGet]
        public async Task<IActionResult> ObtenerDatosCedula(string cedula)
        {
            try
            {
                var response = await _httpClientService.GetHaciendaResponse(cedula);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
