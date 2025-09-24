using CalmSpire.Models;
using Microsoft.EntityFrameworkCore;

namespace CalmSpire.Data
{
    public class CalmSpireDbContext : DbContext
    {
        public CalmSpireDbContext(DbContextOptions<CalmSpireDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<MoodEntry> MoodEntries { get; set; } = default!;
        public DbSet<JournalEntry> JournalEntries { get; set; } = default!;
        public DbSet<Assessment> Assessments { get; set; } = default!;
        public DbSet<AssessmentResult> AssessmentResults { get; set; } = default!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = default!;
        public DbSet<GratitudeEntry> GratitudeEntries { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Email).IsUnique();
                e.Property(x => x.Email).IsRequired().HasMaxLength(255);
            });

            // MoodEntries
            modelBuilder.Entity<MoodEntry>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.MoodEntries).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.HasIndex(x => new { x.UserId, x.CreatedAt });
            });

            // JournalEntries
            modelBuilder.Entity<JournalEntry>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.JournalEntries).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            // Assessments
            modelBuilder.Entity<Assessment>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Title).IsRequired().HasMaxLength(200);
                e.Property(x => x.QuestionsJson).IsRequired();
            });

            // AssessmentResults
            modelBuilder.Entity<AssessmentResult>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.AssessmentResults).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Assessment).WithMany(a => a.Results).HasForeignKey(x => x.AssessmentId).OnDelete(DeleteBehavior.Cascade);
            });

            // ChatMessages
            modelBuilder.Entity<ChatMessage>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.ChatMessages).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.Sender).IsRequired().HasMaxLength(20);
                e.Property(x => x.Message).IsRequired();
            });

            // GratitudeEntries
            modelBuilder.Entity<GratitudeEntry>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.GratitudeEntries).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
