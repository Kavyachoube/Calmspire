using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalmSpire.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to User
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }   // ✅ navigation property

        [Required]
        [MaxLength(20)]
        public string Sender { get; set; } = "user"; // "user" or "bot"

        [Required]
        public string Message { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
