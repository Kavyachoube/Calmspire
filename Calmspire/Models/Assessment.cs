using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalmSpire.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        [Required, StringLength(200)] public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required] public string QuestionsJson { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<AssessmentResult> Results { get; set; } = new List<AssessmentResult>();
    }
}
