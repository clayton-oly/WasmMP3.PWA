using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MP3.API.Models
{
    [Table("UserPush")]
    public class UserPushModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
