using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.APIConsultant.Repositories
{
    public interface IConsultantCalendarRepository
    {
        /// <returns>Returns the availabilites of a consultant from the database</returns>
        /// <param name="id">Id of the consultant</param>
        public Task<List<ConsultantCalendar>> GetConsultantCalendarByConsultantIdAsync(int id);
        /// <returns>Returns the availabilites of the month for a consultant during the given date from the database</returns>
        /// <param name="id">Id of the consultant</param>
        /// <param name="date">Date of month of the availabilities</param>
        public Task<List<ConsultantCalendar>> GetConsultantCalendarOfMonthAsync(int id, DateTime date);
        public Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id);
    }
}