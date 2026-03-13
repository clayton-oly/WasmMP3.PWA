using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MP3.API.Data;
using MP3.API.Models;

namespace MP3.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly MP3DbContext _dbContext;

        public NotificationController(MP3DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterToken([FromBody] UserPushModel user)
        {
            // Verifica se o token já existe para evitar duplicidade
            var exists = await _dbContext.UserPush
                .AnyAsync(t => t.Token == user.Token && t.Name == user.Name);

            if (!exists)
            {
                _dbContext.UserPush.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost("send-all")]
        public async Task<IActionResult> SendToAll([FromBody] string messageText)
        {
            // Busca todos os tokens salvos no SQLite
            var tokens = await _dbContext.UserPush.Select(t => t.Token).ToListAsync();

            if (!tokens.Any()) return BadRequest("Nenhum token encontrado.");

            var message = new MulticastMessage()
            {
                Tokens = tokens,
                Notification = new Notification { Title = "Aviso Geral", Body = messageText }
            };

            var result = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
            return Ok(new { Enviados = result.SuccessCount, Falhas = result.FailureCount });
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendById([FromBody] string messageText, int id)
        {
            // Busca o token pelo id do usuário
            var user = await _dbContext.UserPush.FirstAsync(t => t.Id == id);
            if (user == null) return BadRequest("usuário não encontrado.");

            var token = user.Token;

            var message = new Message()
            {
                Token = token,
                Notification = new Notification { Title = "Aviso", Body = messageText }
            };

            var result = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok("Enviado com sucesso");
        }
    }
}
