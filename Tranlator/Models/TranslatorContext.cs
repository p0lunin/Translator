using Microsoft.EntityFrameworkCore;

namespace Tranlator.Models
{
    public class TranslatorContext : DbContext
    {
        public DbSet<File> Files { get; set; }
        public DbSet<Lang> Langs { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AuthLink> LinksToAuth { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=database.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}