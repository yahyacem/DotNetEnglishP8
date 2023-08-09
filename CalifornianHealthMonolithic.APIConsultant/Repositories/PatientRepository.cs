using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CalifornianHealthMonolithic.APIConsultant.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private CHDBContext DbContext;
        public PatientRepository(CHDBContext dbContext) 
        {
            this.DbContext = dbContext;
        }
        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await DbContext.Patients
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}