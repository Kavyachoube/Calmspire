using System;

namespace Calmspire.Models
{
    public class Lesson
    {
        public int Id { get; set; }   // ✅ Primary Key चाहिए

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
