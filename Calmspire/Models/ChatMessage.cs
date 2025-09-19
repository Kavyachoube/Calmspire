using System.ComponentModel.DataAnnotations;

namespace CalmSpire.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public string Response { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}