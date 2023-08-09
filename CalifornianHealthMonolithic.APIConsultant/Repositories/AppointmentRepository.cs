using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CalifornianHealthMonolithic.APIConsultant.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private CHDBContext DbContext;
        public AppointmentRepository(CHDBContext dbContext) 
        {
            this.DbContext = dbContext;
        }
        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await DbContext.Appointments
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}