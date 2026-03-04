using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MP3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotosController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        //public FotosController(IWebHostEnvironment env)
        //{
        //    _env = env;
        //}

        [HttpPost("upload")]
        public async Task<IActionResult> SalvarFoto([FromBody] string base64Image)
        {   
            if (string.IsNullOrEmpty(base64Image)) return BadRequest("Imagem vazia.");

            // Remove o cabeçalho "data:image/png;base64,"
            var base64Data = base64Image.Split(',')[1];
            var imageBytes = Convert.FromBase64String(base64Data);

            // Define o caminho da pasta (wwwroot/uploads)
            var folderPath = Path.Combine("wwwroot", "uploads");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fileName = $"foto_{Guid.NewGuid()}.png";
            var filePath = Path.Combine(folderPath, fileName);

            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            return Ok(new { url = $"/uploads/{fileName}" });
        }
    }
}
