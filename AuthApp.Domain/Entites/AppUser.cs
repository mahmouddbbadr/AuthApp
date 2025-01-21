using Microsoft.AspNetCore.Identity;

namespace AuthApp.Domain.Entites
{
    public class AppUser : IdentityUser
    {
        public string Role { get; set; }
        public string Address { get; set; }
        public bool IsLocked { get; set; } = false;
    }
}
