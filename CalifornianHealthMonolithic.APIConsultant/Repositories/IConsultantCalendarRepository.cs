using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.APIConsultant.Repositories
{
    public interface IConsultantCalendarRepository
    {
        /// <returns>Returns the availabilites of a consultant from the database</returns>
        /// <param name="id">Id of the consultant</param>
        public Task<List<ConsultantCalendar>> GetConsultantCalendarByConsultantIdAsync(int id);
        /// <returns>Returns the ConsultantCalendar record from the database</returns>
        /// <param name="id">Id of the consultant calendar record</param>
        public Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id);
    }
}