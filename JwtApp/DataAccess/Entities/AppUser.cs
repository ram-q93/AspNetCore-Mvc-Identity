using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace JwtApp.DataAccess.Entities
{
    public class AppUser : IdentityUser
    {
        public string City { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
