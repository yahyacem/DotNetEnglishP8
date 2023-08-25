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
        public async Task<ConsultantCalendar?> GetConsultantCalendarByIdAsync(int id)
        {
            return await DbContext.ConsultantCalendars
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}