using Microsoft.AspNetCore.Identity;

namespace MvcApp.DataAccess.Entities
{
    public class AppUser : IdentityUser
    {
        public string City { get; set; }
    }
}
