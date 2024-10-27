using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using BackendAPIConsumer.Models;  // Importa el espacio de nombres

namespace BackendAPIConsumer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class INEController : ControllerBase
    {
        private readonly string connectionString = "server=localhost;database=EasyEnroll;trusted_connection=false;User Id=sa;Password=zS22004347;Persist Security Info=False;Encrypt=False";
        private readonly HttpClient _httpClient;

        public INEController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeINE([FromBody] ImageRequest request)
        {
            var jsonBody = JsonConvert.SerializeObject(new { image_url = request.ImageUrl });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://127.0.0.1:5000/analyze_ine", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return Content(responseData, "application/json");
            }

            return StatusCode((int)response.StatusCode, "Error al analizar la imagen del INE");
        }

        [HttpPost("registrar")]
public async Task<IActionResult> RegistrarPadreTutor([FromBody] PadreTutor padreTutor)
{
    try
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = "INSERT INTO Padres_Tutores (nombre_padre_tutor, curp_tutor, scan_ine, telefono_padre_tutor, email_padre_tutor, scan_comprobante_domicilio) " +
                        "VALUES (@Nombre, @CURP, @ScanINE, @Telefono, @Email, @ScanComprobanteDomicilio)";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", padreTutor.Nombre ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CURP", padreTutor.CURP ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ScanINE", padreTutor.ScanINE ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Telefono", padreTutor.Telefono ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", padreTutor.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ScanComprobanteDomicilio", padreTutor.ScanComprobanteDomicilio ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }

        return Ok(new { success = true, message = "Registro exitoso" });
    }
    catch (Exception ex)
    {
        // Log detallado del error para depuración
        Console.WriteLine("Error en el registro: " + ex.Message);
        return StatusCode(500, new { success = false, message = "Error al registrar el tutor", error = ex.Message });
    }
}

    }

    public class ImageRequest
    {
        public string ImageUrl { get; set; }
    }
}
