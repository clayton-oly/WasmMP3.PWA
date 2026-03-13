using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MP3.API.Models;

namespace MP3.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class FotosController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;

        public FotosController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> SalvarFoto([FromBody] string base64Image)
        {
            if (string.IsNullOrEmpty(base64Image)) return BadRequest("Imagem vazia.");

            // Remove o cabeçalho "data:image/png;base64,"
            var base64Data = base64Image.Split(',')[1];
            var imageBytes = Convert.FromBase64String(base64Data);

            // Define o caminho da pasta (wwwroot/uploads)
            //var folderPath = Path.Combine(_env.WebRootPath, "uploads");
            var folderPath = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads");

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fileName = $"foto_{Guid.NewGuid()}.png";
            var filePath = Path.Combine(folderPath, fileName);

            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            return Ok(new { url = $"/uploads/{fileName}" });
        }



        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Arquivo inválido.");

            var folderPath = Path.Combine("wwwroot/uploads");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fileName = $"nativa_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok();
        }



        [HttpPost("upload-form")]
        public async Task<IActionResult> UploadForm([FromForm]UploadModel file)
        {
            if (file == null || file.Arquivo.Length == 0) return BadRequest("Arquivo inválido.");

            var folderPath = Path.Combine($"wwwroot/uploads/{file.Pasta}");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fileName = $"{file.Nome}{Path.GetExtension(file.Arquivo.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.Arquivo.CopyToAsync(stream);
            }

            return Ok();
        }


    }
}
