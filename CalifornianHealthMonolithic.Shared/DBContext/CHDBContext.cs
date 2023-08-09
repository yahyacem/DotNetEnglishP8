using CalifornianHealthMonolithic.Shared.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CalifornianHealthMonolithic.Shared.Models.Identity;

namespace CalifornianHealthMonolithic.Shared.DBContext
{
    public class CHDBContext : IdentityDbContext<Patient, AppRole, int>
    {
        public CHDBContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Patient>(entity =>
            {
                entity.ToTable(name: "Patient");
            });
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<ConsultantCalendar> ConsultantCalendars { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}