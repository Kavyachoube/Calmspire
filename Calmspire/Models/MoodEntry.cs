using System;
using System.ComponentModel.DataAnnotations;

namespace CalmSpire.Models
{
    public class MoodEntry
    {
        public int Id { get; set; }
        [Required] public int UserId { get; set; }
        [Range(1, 10)] public int MoodScore { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
    }
}
