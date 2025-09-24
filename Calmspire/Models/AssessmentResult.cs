using System;

namespace CalmSpire.Models
{
    public class AssessmentResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int AssessmentId { get; set; }
        public Assessment? Assessment { get; set; }
        public string ResponsesJson { get; set; } = string.Empty;
        public int? Score { get; set; }
        public string? Interpretation { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    }
}
