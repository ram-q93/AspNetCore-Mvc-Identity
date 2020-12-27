using Auth.DataAccess;
using Auth.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));


            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();


            //Apply Authorize attribute globally
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //---------------part-95-----------------
            //Role based authorization(RBAC) vs claims based authorization(CBAC)
            //Authorization is the process of determining what the logged-in user 
            //can and cannot do. Based on our application authorization requirements we 
            //could use Roles, Claims or a combination of both.

            //A claim is a name - value pair.It's really a piece of information about the user, 
            //not what the user can and cannot do. For example username, email, age, gender etc are
            //all claims. How you use these claims for authorization checks in your application is up to 
            //your application business and authorization requirements. 

            //For example, if you are building an employee portal you may allow the logged-in user to 
            //apply for a maternity leave if the gender claim value is female.Similarly, if you are building 
            //an ecommerce application, you may allow the logged -in user to submit an order if the age claim
            //value is greater than or equal to 18.

            //Claims are policy based. We create a policy and include one or more claims in that policy. 
            //The policy is then used along with the policy parameter of the Authorize attribute to implement
            //claims based authorization.

            // In ASP.NET Core, a role is just a claim with type Role. 

            services.AddAuthorization(options =>
            {   //To satisfy this policy requirements, the loggedin user must have both claims
                options.AddPolicy("DeleteRolePolicy", policy =>
                                policy.RequireClaim("Delete Role")
                                      .RequireClaim("Create Role"));

                options.AddPolicy("AdminUserRolePolicy", policy => policy.RequireRole("Admin", "User"));
                options.AddPolicy("EditRolePolicy", policy => policy.RequireClaim("Edit Role"));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
