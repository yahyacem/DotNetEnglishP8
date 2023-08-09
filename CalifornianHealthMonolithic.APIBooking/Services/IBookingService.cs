using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.APIBooking.Models;

namespace CalifornianHealthMonolithic.APIBooking.Services
{
    public interface IBookingService
    {
        ///<summary>Book a new appointment and sets the given ConsultantCalendar record as unavailable</summary>
        ///<returns>Returns the created appointment</returns>
        ///<param name="patientId">Id of the patient that books the appointment</param>
        ///<param name="consultantCalendar">Id of the ConsultantCalendar record to book</param>
        public Task<BookAppointmentResponseModel> BookAppointmentAsync(int patientId, int consultantCalendarId);
    }
}