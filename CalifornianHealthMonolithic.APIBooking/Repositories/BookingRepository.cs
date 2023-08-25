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
    }
}