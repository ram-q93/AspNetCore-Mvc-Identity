using Microsoft.AspNetCore.Identity;

namespace JwtApp.DataAccess.Entities
{
    public class AppUser : IdentityUser
    {
        public string City { get; set; }
    }
}
