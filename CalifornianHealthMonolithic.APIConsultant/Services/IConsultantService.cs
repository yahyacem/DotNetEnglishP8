using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.APIConsultant.Services
{
    public interface IConsultantService
    {
        /// <summary>Get list of all consultants</summary>
        /// <returns>Returns list of all Consultant records</returns>
        public Task<List<Consultant>> GetConsultantsAsync();
        /// <summary>Get list of names of all consultants</summary>
        /// <returns>Returns list of names all Consultant records</returns>
        public Task<List<ConsultantNameModel>> GetConsultantsNamesAsync();
        /// <summary>Get all the consultant calendar entries</summary>
        /// <param name="id">Id of the consultant</param>
        /// <returns>Returns all ConsultantCalendar records of the specified Consultant</returns>
        public Task<List<ConsultantCalendar>> GetConsultantCalendarByConsultantIdAsync(int id);
        /// <summary>Get ConsultantCalendar by Id</summary>
        /// <param name="id">Id of ConsultantCalendar record</param>
        /// <returns>Returns ConsultantCalendar record</returns>
        public Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id);
        /// <summary>Get appointment entry by Id</summary>
        /// <param name="id">Id of Appointment</param>
        /// <returns>Returns AppointmentModel mapped from the Appointment record</returns>
        public Task<AppointmentViewModel?> GetAppointmentModelByIdAsync(int id);
    }
}