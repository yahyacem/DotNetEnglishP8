using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CalifornianHealthMonolithic.APIConsultant.Repositories
{
    public class ConsultantCalendarRepository : IConsultantCalendarRepository
    {
        private CHDBContext DbContext;
        public ConsultantCalendarRepository(CHDBContext dbContext) 
        {
            this.DbContext = dbContext;
        }
        public async Task<List<ConsultantCalendar>> GetConsultantCalendarByConsultantIdAsync(int id)
        {
            return await DbContext.ConsultantCalendars
                .Where(x => x.ConsultantId == id && x.Available).ToListAsync();
        }
        public async Task<List<ConsultantCalendar>> GetConsultantCalendarOfMonthAsync(int id, DateTime date)
        {
            return await DbContext.ConsultantCalendars
                .Where(x => x.ConsultantId == id && x.Available && x.Date.Month == date.Month).ToListAsync();
        }
        public async Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id)
        {
            return await DbContext.ConsultantCalendars
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        // public async Task<bool> AddAppointmentAsync(Appointment model)
        // {
        //     if (model == null)
        //         return false;

        //     await DbContext.Appointments.AddAsync(model);
        //     await DbContext.SaveChangesAsync();
        //     return true;
        // }
    }
}