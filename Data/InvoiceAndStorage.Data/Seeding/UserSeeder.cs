namespace InvoiceAndStorage.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InvoiceAndStorage.Common;
    using InvoiceAndStorage.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Users.Any() && !dbContext.UserRoles.Any())
            {
                var userAdmin = new ApplicationUser
                {
                    FirstName = "Pesho",
                    LastName = "Administratora",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                };

                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                await userManager.CreateAsync(userAdmin, "123456");

                var admin = dbContext.Roles.FirstOrDefault(x => x.Name == GlobalConstants.AdministratorRoleName);
                var user = dbContext.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");

                await dbContext.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = user.Id, RoleId = admin.Id });
            }
        }
    }
}
