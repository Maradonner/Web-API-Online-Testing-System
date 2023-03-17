using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TestingSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpired { get; set; }
        [ForeignKey("Role")]
        public int? RoleId { get; set; } = 1;
        public Role? Role { get; set; }
        [NotMapped]
        public virtual List<TriviaQuiz>? TriviaQuiz { get; set; }
    }
}
