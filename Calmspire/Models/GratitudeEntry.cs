using System;
using System.ComponentModel.DataAnnotations;

namespace CalmSpire.Models
{
    public class GratitudeEntry
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please write something you are grateful for.")]
        [StringLength(500)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public User? User { get; set; }
    }
}
