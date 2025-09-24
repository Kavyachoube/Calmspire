using System;
using System.Collections.Generic;

namespace CalmSpire.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }

        // navigation
        public ICollection<MoodEntry> MoodEntries { get; set; } = new List<MoodEntry>();
        public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
        public ICollection<AssessmentResult> AssessmentResults { get; set; } = new List<AssessmentResult>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public ICollection<GratitudeEntry> GratitudeEntries { get; set; } = new List<GratitudeEntry>();
    }
}
