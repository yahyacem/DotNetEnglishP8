using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CalifornianHealthMonolithic.APIConsultant.Repositories
{
    public class ConsultantRepository : IConsultantRepository
    {
        private CHDBContext DbContext;
        public ConsultantRepository(CHDBContext dbContext) 
        {
            this.DbContext = dbContext;
        }
        public async Task<Consultant?> GetConsultantByIdAsync(int id)
        {
            return await DbContext.Consultants
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Consultant>> GetConsultantsAsync()
        {
            return await DbContext.Consultants.ToListAsync();
        }
        public async Task<List<ConsultantNameModel>> GetConsultantNamesAsync()
        {
            return await DbContext.Consultants
                .Select(s => new ConsultantNameModel()
                {
                    Id = s.Id,
                    Name = $"{s.FName} {s.LName}"
                }).ToListAsync();
        }
    }
}