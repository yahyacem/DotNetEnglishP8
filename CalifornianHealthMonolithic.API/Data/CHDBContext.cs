using CalifornianHealthMonolithic.Shared;
using Microsoft.EntityFrameworkCore;

namespace CalifornianHealthMonolithic.API.Data
{
    public class CHDBContext : DbContext
    {
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Consultant> consultants { get; set; }
        public DbSet<ConsultantCalendar> consultantCalendars { get; set; }
        public DbSet<Patient> patients { get; set; }
    }
}