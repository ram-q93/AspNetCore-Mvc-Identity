using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using MvcApp.DataAccess.Entities;
using MvcApp.Utilities;

namespace MvcApp.DataAccess
{
    public class Seed
    {
        public static async Task SeedData(
            AppIdentityDbContext _context,
            UserManager<AppUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {


            List<IdentityRole> roles = null;


            if (!_roleManager.Roles.Any())
            {
                roles = new List<IdentityRole>{
                    new IdentityRole{Name=Constants.SuperAdmin},
                    new IdentityRole{Name=Constants.Admin},
                    new IdentityRole{Name=Constants.User}
                };

                //Adding roles
                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }
            }


            if (!_userManager.Users.Any())
            {

                var superAdminUser = new AppUser
                {
                    UserName = "superadmin@test.com",
                    Email = "superadmin@test.com",
                    City = "London",
                    EmailConfirmed = true,

                };
                await AddUserWithRole(_userManager, superAdminUser, Constants.SuperAdmin);
                await AddUserClaims(_userManager, superAdminUser, Constants.SuperAdmin);


                var adminUser = new AppUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    City = "Tehran",
                    EmailConfirmed = true
                };
                await AddUserWithRole(_userManager, adminUser, Constants.Admin);


                var simpleUser = new AppUser
                {
                    UserName = "user@test.com",
                    Email = "user@test.com",
                    City = "Copenhagen",
                    EmailConfirmed = true
                };
                await AddUserWithRole(_userManager, simpleUser, Constants.User);
            };



        }

        private static async Task AddUserWithRole(UserManager<AppUser> _userManager,
            AppUser user, string roleConstant)
        {
            //Add user
            await _userManager.CreateAsync(user: user, password: "test");

            //Add role to user
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRoleAsync(user, roleConstant);
        }

        private static async Task AddUserClaims(UserManager<AppUser> _userManager,
            AppUser user, string roleConstant)
        {
            // Get all the user existing claims and delete them
            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            var superAdminClaims = new List<Claim>{
                new Claim(Constants.CreateRole, Constants.True),
                new Claim(Constants.EditRole, Constants.True),
                new Claim(Constants.DeleteRole, Constants.True),
                new Claim(Constants.ManageUserClaims, Constants.True),
                new Claim(Constants.Create, Constants.True),
                new Claim(Constants.Edit, Constants.True),
                new Claim(Constants.Delete, Constants.True)
            };
            await _userManager.AddClaimsAsync(user, superAdminClaims);
        }
    }




}


