using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendAPIConsumer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class INEController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public INEController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeINE([FromBody] ImageRequest request)
        {
            // Construir el contenido de la petición
            var jsonBody = JsonConvert.SerializeObject(new { image_url = request.ImageUrl });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Hacer la petición a la API externa
            var response = await _httpClient.PostAsync("http://127.0.0.1:5000/analyze_ine", content);

            // Verificar si la respuesta es exitosa
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return Content(responseData, "application/json");
            }

            // Manejar errores
            return StatusCode((int)response.StatusCode, "Error al analizar la imagen del INE");
        }
    }

    public class ImageRequest
    {
        public string ImageUrl { get; set; }
    }
}
