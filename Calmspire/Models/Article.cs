namespace CalmSpire.Models
{
    public class Article
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = "#";
        public string? UrlToImage { get; set; }
    }
}
