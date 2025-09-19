namespace CalmSpire.Models.ViewModels
{
    public class DashboardViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public List<MoodEntry> RecentMoods { get; set; } = new List<MoodEntry>();
        public int TotalJournalEntries { get; set; }
        public int AssessmentsCompleted { get; set; }
        public DateTime? LastMoodEntry { get; set; }
        public double? AverageMoodThisWeek { get; set; }
    }
}