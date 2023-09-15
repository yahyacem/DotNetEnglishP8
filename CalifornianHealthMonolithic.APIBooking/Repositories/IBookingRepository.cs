using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.APIBooking.Repositories
{
    public interface IBookingRepository
    {
        /// <summary>Creates an appointment and returns record</summary>
        /// <returns>Returns an appointments created</returns>
        /// <param name="appointment">Appointment to create</param>
        public Task<Appointment?> CreateAppointmentAsync(Appointment appointment);
        /// <returns>Returns a ConsultantCalendar record by id from the database</returns>
        /// <param name="id">Id of the ConsultantCalendar record</param>
        public Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id);
        /// <summary>Update a record of "ConsultantCalendar" in the database</summary>
        /// <returns>Updated record of "ConsultantCalendar" from the database</returns>
        /// <param name="consultantCalendar">ConsultantCalendar object to update</param>
        public Task<ConsultantCalendar?> UpdateConsultantCalendarAsync(ConsultantCalendar consultantCalendar);
        /// <summary>Get the Consultant object with the specified Id from the database</summary>
        /// <param name="id">Id of the consultant to return</param>
        /// <returns>Returns the Consultant object with the specified Id</returns>
        public Task<Consultant> GetConsultantByIdAsync(int id);
        /// <summary>Get the Patient object with the specified Id from the database</summary>
        /// <param name="id">Id of the patient to return</param>
        /// <returns>Returns the Patient object with the specified Id</returns>
        public Task<Patient> GetPatientByIdAsync(int id);
        /// <summary>Set ConsultantCalendar record to not available and create a new appointment</summary>
        /// <param name="consultantCalendarId"></param>
        /// <param name="patientId"></param>
        /// <returns>Returnes the new appointment created. If any error, it will return null.</returns>
        public Task<Appointment?> BookAppointmentAsync(int consultantCalendarId, int patientId);
    }
}