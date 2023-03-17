using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TestingSystem.Models;

namespace TestingSystem.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<TriviaOption> TriviaOptions { get; set; }
        public DbSet<TriviaQuestion> TriviaQuestions { get; set; }
        public DbSet<TriviaQuiz> TriviaQuizs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ActiveTrivia> ActiveTrivias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasOne(x => x.Role)
             .WithMany()
             .HasForeignKey(x => x.RoleId)
             .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
