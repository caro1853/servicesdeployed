using System;
using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.Common;
using Scheduling.Domain.Entities;

namespace Scheduling.Infrastructure.Persistence
{
	public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OperationalHour>().HasIndex(x => new { x.DoctorId, x.Day }).IsUnique();
            builder.Entity<Hour>().HasIndex(x => new { x.Schedule, x.OperationalHourId }).IsUnique();
          
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "ccv";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "ccv";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<OperationalHour> OperationalHours { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Appointment> Appointments { get; set; }


    }
}

