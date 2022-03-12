using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistance
{
    public class DevJobContext : DbContext
    {
        public DevJobContext(DbContextOptions<DevJobContext> options) : base(options)
        {

        }
        
        public DbSet<JobVacancy> JobVacancies { get; set;}
        public DbSet<JobApplication> JobApplications { get; set;}

        //Fluent API
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<JobVacancy>(e => {
                e.HasKey(jv => jv.Id);
                e.HasMany(jv => jv.Applications)
                .WithOne()
                .HasForeignKey(ja => ja.IdJobVacancy)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<JobApplication>(e => {
                e.HasKey(ja => ja.Id);
            });
        }
    }
}