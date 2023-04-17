using CalifornianHealthMonolithic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalifornianHealthMonolithic.Code
{
    public class Repository
    {
    
        public List<Consultant> FetchConsultants(CHDBContext dbContext)
        {
            var cons = dbContext.consultants.ToList();
            return cons;
        }

        public List<ConsultantCalendar> FetchConsultantCalendars(CHDBContext dbContext)
        {
            //Should the consultant detail and the calendar (available dates) be clubbed together?
            //Is this the reason the calendar is slow to load? Rethink how we can rewrite this?

            return dbContext.consultantCalendars.ToList();
        }

        public bool CreateAppointment(Appointment model, CHDBContext dbContext)
        {
            //Should we double check here before confirming the appointment?
            dbContext.appointments.Add(model);
            return true;
        }
    }
}