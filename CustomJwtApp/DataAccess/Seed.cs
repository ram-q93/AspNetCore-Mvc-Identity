using AspNetCore.Lib.Services.Interfaces;
using CustomJwtApp.Core;
using CustomJwtApp.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomJwtApp.DataAccess
{
    public class Seed
    {
        public static async Task SeedData(DataContext _context, ICryptoService _cryptoService)
        {
            if (!_context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        FirstName = "Bob",
                        LastName="Anderson",
                        Enabled=true,
                        Username ="bob",
                        Password=_cryptoService.ComputeSha512Hash("test"),
                        Role = new Role
                        {
                            Name = Roles.Admin.ToString(),
                            //Permissions = AdminPermissions()
                        }

                    }
                    //,
                    //new User
                    //{
                    //    FirstName = "Tom",
                    //    LastName="Willey",
                    //    Enabled=true,
                    //    Username ="tom",
                    //   Password=cryptoService.ComputeSha512Hash("test")
                    //},
                    //new User
                    //{
                    //    FirstName = "Anna",
                    //    LastName="Jefferson",
                    //    Enabled=true,
                    //    Username ="anna",
                    //   Password=cryptoService.ComputeSha512Hash("test")
                    //},
                };

                _context.AddRange(users);

                await _context.SaveChangesAsync();
            }
        }
        private static List<Permission> AdminPermissions()
        {
            return new List<Permission>
            {
                new Permission{Id = (int)Permissions.Create, Name = Permissions.Create.ToString()}
            };
        }
    }

}