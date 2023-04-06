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
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasOne(x => x.Role)
             .WithMany()
             .HasForeignKey(x => x.RoleId)
             .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
