using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.APIConsultant.Repositories
{
    public interface IConsultantRepository
    {
        /// <returns>Returns a consultant from the database</returns>
        /// <param name="id">Id of the consultant</param>
        public Task<Consultant?> GetConsultantByIdAsync(int id);
        /// <returns>Returns the list of consultants from the database</returns>
        public Task<List<Consultant>> GetConsultantsAsync();
        /// <returns>Returns the list of names of the consultants from the database</returns>
        public Task<List<ConsultantNameModel>> GetConsultantNamesAsync();
    }
}