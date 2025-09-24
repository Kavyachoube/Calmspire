namespace CalmSpire.Models
{
    public class ExternalArticle
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Url { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    }

    public class ExternalVideo
    {
        public string VideoId { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string ThumbnailUrl { get; set; } = "";
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    }

    public class JournalIndexViewModel
    {
        public List<ExternalArticle> Articles { get; set; } = new();
        public List<ExternalVideo> Videos { get; set; } = new();
        public List<JournalEntry> LocalEntries { get; set; } = new();
    }
}
