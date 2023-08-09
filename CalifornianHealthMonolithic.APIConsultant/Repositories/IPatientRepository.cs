using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;

namespace CalifornianHealthMonolithic.APIConsultant.Repositories
{
    public interface IPatientRepository
    {
        /// <returns>Returns a patient from the database</returns>
        /// <param name="id">Id of the patient</param>
        public Task<Patient?> GetPatientByIdAsync(int id);
    }
}