using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Tests;
using Microsoft.EntityFrameworkCore;

namespace CalifornianHealthMonolithic.Tests.Integration
{
    public class IntegrationBase : TestBase
    {
        public IntegrationBase() : base() {}
        protected static DbContextOptions<CHDBContext> GetOptions()
        {
            return new DbContextOptionsBuilder<CHDBContext>()
            .UseInMemoryDatabase("ProductServiceRead" + Guid.NewGuid().ToString())
            .Options;
        }
    }
}