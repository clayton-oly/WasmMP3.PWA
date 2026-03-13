namespace MP3.API.Models
{
    public class UploadModel
    {

        public IFormFile Arquivo { get; set; }
        public string Nome { get; set; }
        public string Pasta { get; set; }

    }
}
