using Calmspire.Models;
using CalmSpire.Models;
using Microsoft.EntityFrameworkCore;

namespace CalmSpire.Data
{
    public class CalmSpireDbContext : DbContext
    {
        public CalmSpireDbContext(DbContextOptions<CalmSpireDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MoodEntry> MoodEntries { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentResult> AssessmentResults { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<GratitudeEntry> GratitudeEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User configurations
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
            });

            // MoodEntry configurations
            modelBuilder.Entity<MoodEntry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.MoodEntries)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(e => new { e.UserId, e.EntryDate }).IsUnique();
            });

            // JournalEntry configurations
            modelBuilder.Entity<JournalEntry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.JournalEntries)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Content).IsRequired();
            });

            // Assessment configurations
            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.QuestionsJson).IsRequired();
            });

            // AssessmentResult configurations
            modelBuilder.Entity<AssessmentResult>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.AssessmentResults)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Assessment)
                    .WithMany(a => a.Results)
                    .HasForeignKey(e => e.AssessmentId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.ResponsesJson).IsRequired();
            });

            // ChatMessage configurations
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.ChatMessages)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.Response).IsRequired();
            });

            // GratitudeEntry configurations
            modelBuilder.Entity<GratitudeEntry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.GratitudeEntries)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.Content).IsRequired().HasMaxLength(500);
                entity.HasIndex(e => new { e.UserId, e.EntryDate }).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}