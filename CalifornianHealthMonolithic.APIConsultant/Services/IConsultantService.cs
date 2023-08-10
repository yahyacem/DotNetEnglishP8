using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.APIConsultant.Services
{
    public interface IConsultantService
    {
        public Task<List<Consultant>> GetConsultantsAsync();
        public Task<List<ConsultantNameModel>> GetConsultantsNamesAsync();
        public Task<List<ConsultantCalendar>> GetConsultantCalendarByConsultantIdAsync(int id);
        public Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id);
        public Task<List<ConsultantCalendar>> GetConsultantCalendarOfMonthAsync(int id, DateTime date);
        public Task<AppointmentViewModel?> GetAppointmentViewModelByIdAsync(int id);
    }
}