using Microsoft.AspNetCore.Identity;

namespace TradingProject.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public required string FullName { get; set; } 
    }
}
