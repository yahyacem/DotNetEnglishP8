using Microsoft.AspNetCore.Identity;

namespace CalifornianHealthMonolithic.Shared.Models.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole() : base() { }
        public AppRole(string roleName)
        {
            Name = roleName;
        }
    }
}