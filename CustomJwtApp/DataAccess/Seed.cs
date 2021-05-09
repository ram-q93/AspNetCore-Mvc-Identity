using AspNetCore.Lib.Services.Interfaces;
using CustomJwtApp.Core;
using CustomJwtApp.DataAccess;
using CustomJwtApp.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, ICryptoService cryptoService)
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        FirstName = "Bob",
                        LastName="Anderson",
                        Enabled=true,
                        Username ="bob",
                        Password=cryptoService.ComputeSha512Hash("test"),
                        Role = new Role
                        {
                            Name = Roles.Admin.ToString(),
                            // RolePermissions = new List<RolePermission>
                            // {
                            //     new RolePermission
                            //     {
                            //         Permission =new Permission
                            //         {
                            //             Id = (int)Permissions.Create,
                            //             Name = Permissions.Create.ToString()
                            //         }
                            //     }
                            // }
                            
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

                foreach (var user in users)
                {
                    // await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                await context.SaveChangesAsync();
            }
        }
    }
}