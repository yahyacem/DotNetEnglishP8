using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.APIBooking.Repositories
{
    public interface IBookingRepository
    {
        /// <returns>Returns an appointments by id from the database</returns>
        /// <param name="id">Id of the appointment</param>
        public Task<Appointment?> GetAppointmentByIdAsync(int id);
        /// <summary>Creates an appointment and returns record</summary>
        /// <returns>Returns an appointments created</returns>
        /// <param name="appointment">Appointment to create</param>
        public Task<Appointment?> CreateAppointmentAsync(Appointment appointment);
        /// <returns>Returns a ConsultantCalendar record by id from the database</returns>
        /// <param name="id">Id of the ConsultantCalendar record</param>
        public Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id);
        /// <returns>Update a record of "ConsultantCalendar" in the database</returns>
        /// <param name="id">Id of the consultant</param>
        /// <param name="date">Date of month of the availabilities</param>
        public Task<ConsultantCalendar?> UpdateConsultantCalendarAsync(ConsultantCalendar consultantCalendar);
        public Task<Consultant> GetConsultantByIdAsync(int id);
        public Task<Patient> GetPatientByIdAsync(int id);
    }
}