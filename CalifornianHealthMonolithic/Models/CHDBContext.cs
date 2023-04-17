using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CalifornianHealthMonolithic.Models
{
    public class CHDBContext : DbContext
    {
        public DbSet<Appointment> appointments { get; set; }

        public DbSet<Consultant> consultants { get; set; }

        public DbSet<ConsultantCalendar> consultantCalendars { get; set; }

        public DbSet<Patient> patients { get; set; }

    }
}