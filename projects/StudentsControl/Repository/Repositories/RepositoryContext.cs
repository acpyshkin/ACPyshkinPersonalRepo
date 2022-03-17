using System;
using Domain;
using Microsoft.EntityFrameworkCore;
namespace Repository
{
    internal class RepositoryContext : DbContext
    {
        public DbSet<LectureEntity> Lectures { get; set; }
        public DbSet<LecturerEntity> Lecturers { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<HomeWorkEntity> HomeWorks { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
/*
            StudentEntity tom = new StudentEntity { Id = Guid.NewGuid(), Name = "Tom" };
            StudentEntity alice = new StudentEntity { Id = Guid.NewGuid(), Name = "Alice" };
            StudentEntity bob = new StudentEntity { Id = Guid.NewGuid(), Name = "Bob" };
            StudentEntity sam = new StudentEntity { Id = Guid.NewGuid(), Name = "Sam" };
            StudentEntity alfred = new StudentEntity { Id = Guid.NewGuid(), Name = "Alfred" };
            StudentEntity samantha = new StudentEntity { Id = Guid.NewGuid(), Name = "Samantha" };
            Students.AddRange(tom, alice, bob, sam, samantha, alfred);

            LectureEntity algorithms = new LectureEntity { Id = Guid.NewGuid(), Topic = "Algorithms", DateOfLecture = new DateTime(2020, 5, 10) };
            LectureEntity dotnet = new LectureEntity { Id = Guid.NewGuid(), Topic = "Dotnet", DateOfLecture = new DateTime(2020, 6, 20) };
            LectureEntity math = new LectureEntity { Id = Guid.NewGuid(), Topic = "Math", DateOfLecture = new DateTime(2020, 7, 21) };
            Lectures.AddRange(algorithms, dotnet, math);

            tom.LectureAttandance.Add(algorithms);
            tom.LectureAttandance.Add(dotnet);
            alice.LectureAttandance.Add(algorithms);
            alice.LectureAttandance.Add(math);
            bob.LectureAttandance.Add(dotnet);
            bob.LectureAttandance.Add(math);
            sam.LectureAttandance.Add(algorithms);
            alfred.LectureAttandance.Add(dotnet);
            samantha.LectureAttandance.Add(dotnet);
            samantha.LectureAttandance.Add(algorithms);
            samantha.LectureAttandance.Add(math);

            SaveChanges();*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LectureEntity>()
                .HasMany(s => s.StudentsThatAttend)
                .WithMany(l => l.LectureAttandance)
                .UsingEntity(j => j.ToTable("Attandance"));

            modelBuilder.Entity<StudentEntity>()
                .HasMany(h => h.AppointedHomeWorksList)
                .WithOne(s => s.AssignedStudent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HomeWorkEntity>()
                .HasOne(l => l.RelevantLecture)
                .WithMany(h => h.SubmitedHomeWorks)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseEntity>()
                .HasMany(s => s.AppointedStudentsList)
                .WithOne(c => c.Course)
                .HasForeignKey(s => s.CourseID);

            modelBuilder.Entity<CourseEntity>()
                .HasMany(l => l.AppointedLecturesList)
                .WithOne(c => c.Course)
                .HasForeignKey(l => l.CourseID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseEntity>()
                .HasOne(lctr => lctr.Lecturer)
                .WithOne(c => c.Course)
                .HasForeignKey<LecturerEntity>(l => l.CourseId);
        }

    }
}