using Microsoft.AspNetCore.Identity;

namespace Auth.DataAccess.Entities
{
    public class AppUser : IdentityUser
    {
        public string City { get; set; }
    }
}
