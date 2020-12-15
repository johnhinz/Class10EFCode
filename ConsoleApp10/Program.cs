using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    public class SchoolContext : DbContext
    {

        public DbSet<CoursePoco> Courses { get; set; }
        public DbSet<StudentPoco> Students { get; set; }
        public DbSet<TeacherPoco> Teachers { get; set; }
        public DbSet<CourseStudentPoco> CourseStudents { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=CSHARPHUMBER\HUMBERBRIDGING;Initial Catalog=SCHOOL;Integrated Security=True;"
                );
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CourseStudentPoco>()
                .HasKey(cs => new { cs.Student, cs.Course }); 

            modelBuilder.Entity<TeacherPoco>()
                .HasKey(t => t.MumboJumo );
            modelBuilder.Entity<StudentPoco>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<CourseStudentPoco>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(cs => cs.StudentId);

            modelBuilder.Entity<CourseStudentPoco>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<CourseStudentPoco>()
                .HasOne<TeacherPoco>();

            base.OnModelCreating(modelBuilder);
        }

    }

    public class CourseStudentPoco
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public StudentPoco Student { get; set; }
        public CoursePoco Course { get; set; }
    }

    // courses
    public class CoursePoco
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Fee { get; set; }
        public List<CourseStudentPoco> Students { get; set; }
    }

    // teachers 
    public class TeacherPoco
    {
        public Guid MumboJumo { get; set; }
        public string Name { get; set; }
        List<CoursePoco> Courses { get; set; }
    }

    // students
    public class StudentPoco
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CourseStudentPoco> Courses { get; set; }

    }

}
