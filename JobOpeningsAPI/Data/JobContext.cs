using JobOpeningsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobOpeningsAPI.Data
{
    public class JobContext : DbContext
    {
        public JobContext()
        {
        }
        public JobContext(DbContextOptions<JobContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("A FALLBACK CONNECTION STRING");
            }
        }
        public DbSet<Job> Job { get; set; }
        public DbSet<Locations> Location { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId);

                entity.Property(e => e.DeptTitle)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("title");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.Property(e => e.LocationTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.LocationCity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.LocationState)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("state");

                entity.Property(e => e.LocationCountry)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.LocationZipCode)
                .HasColumnName("zip");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.Property(e => e.JobCode)
                   .HasMaxLength(10)
                   .IsUnicode(false)
                   .HasDefaultValueSql("CONCAT('JOB-', NEXT VALUE FOR JobCodeSequence)")
                   .HasColumnName("code");

                entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");

                entity.Property(e => e.JobDescription)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.JobPostedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("postedDate");

                entity.Property(e => e.JobClosingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("closingDate");
            });

        }

            
    }
}
