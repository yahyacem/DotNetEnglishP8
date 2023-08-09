using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.APIConsultant.Repositories
{
    public interface IAppointmentRepository
    {
        /// <returns>Returns an appointment record by id from the database</returns>
        /// <param name="id">Id of the appointment</param>
        public Task<Appointment?> GetAppointmentByIdAsync(int id);
    }
}