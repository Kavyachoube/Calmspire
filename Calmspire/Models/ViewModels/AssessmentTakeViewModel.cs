using System.Collections.Generic;

namespace CalmSpire.Models.ViewModels
{
    public class QuestionDto
    {
        public string Question { get; set; } = "";
        public string Type { get; set; } = "radio"; // radio / checkbox / text
        public List<string> Options { get; set; } = new();
    }

    public class AssessmentTakeViewModel
    {
        public int AssessmentId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
