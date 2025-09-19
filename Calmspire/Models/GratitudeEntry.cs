using System.ComponentModel.DataAnnotations;

namespace CalmSpire.Models
{
    public class GratitudeEntry
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Content { get; set; } = string.Empty;

        public DateTime EntryDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}