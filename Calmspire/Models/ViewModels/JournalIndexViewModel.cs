using System.Collections.Generic;

namespace CalmSpire.Models.ViewModels
{
    public class JournalIndexViewModel
    {
        public List<Article> Articles { get; set; } = new();
        public List<JournalEntry> LocalEntries { get; set; } = new();
    }
}
