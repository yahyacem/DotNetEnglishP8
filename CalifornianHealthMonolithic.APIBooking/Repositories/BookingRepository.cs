using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using Microsoft.EntityFrameworkCore;
using CalifornianHealthMonolithic.Shared.DBContext;
using AutoMapper;

namespace CalifornianHealthMonolithic.APIBooking.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CHDBContext DbContext;
        public BookingRepository(CHDBContext dbContext) 
        {
            this.DbContext = dbContext;
        }
        public async Task<Appointment?> CreateAppointmentAsync(Appointment appointment)
        {
            if (appointment != null)
            {
                await DbContext.Appointments.AddAsync(appointment);
                await DbContext.SaveChangesAsync();
            }
            return appointment;
        }
        public async Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id)
        {
            return await DbContext.ConsultantCalendars
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ConsultantCalendar?> UpdateConsultantCalendarAsync(ConsultantCalendar consultantCalendar)
        {
            if (consultantCalendar != null)
            {
                DbContext.ConsultantCalendars.Update(consultantCalendar);
                await DbContext.SaveChangesAsync();
            }
            return consultantCalendar;
        }
        public async Task<Consultant> GetConsultantByIdAsync(int id)
        {
            return await DbContext.Consultants
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await DbContext.Patients
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Appointment?> BookAppointmentAsync(int consultantCalendarId, int patientId)
        {
            // Begin transaction and lock table
            using var dbContextTransaction = DbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable);
            
            try
            {
                // Check if ConsultantCalendar provided exists and is available
                ConsultantCalendar? consultantCalendarToBook = await DbContext.ConsultantCalendars.FirstOrDefaultAsync(x => x.Id == consultantCalendarId);
                if (consultantCalendarToBook == null || !consultantCalendarToBook.Available)
                {
                    return null;
                }

                // Set record to not available and update it in database
                consultantCalendarToBook.Available = false;
                DbContext.ConsultantCalendars.Update(consultantCalendarToBook);
                await DbContext.SaveChangesAsync();

                // Create new appointment and insert it to database
                Appointment newAppointment = new()
                {
                    PatientId = patientId,
                    ConsultantId = consultantCalendarToBook.ConsultantId,
                    StartDateTime = consultantCalendarToBook.Date,
                    EndDateTime = consultantCalendarToBook.Date.AddHours(1)
                };
                await DbContext.Appointments.AddAsync(newAppointment);
                await DbContext.SaveChangesAsync();

                // Commit transaction to perform all the changes to the database
                dbContextTransaction.Commit();

                return newAppointment;
            } catch
            {
                return null;
            }
        }
    }
}