using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace emre_yasin_salli_thesis.Areas.Identity.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            var pwd = "Admin.1234";
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            // Seed Roles
            var adminRole = new IdentityRole("admin");
            adminRole.NormalizedName = adminRole.Name.ToUpper();

            var memberRole = new IdentityRole("member");
            memberRole.NormalizedName = memberRole.Name.ToUpper();

            List<IdentityRole> roles = new List<IdentityRole>() {
            adminRole,
            memberRole
            };

            builder.Entity<IdentityRole>().HasData(roles);


            // Seed Users
            var adminUser = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@email.com",
                Email = "admin@email.com",
                PasswordHash = passwordHasher.HashPassword(null, pwd),
            };
            adminUser.NormalizedUserName = adminUser.UserName.ToUpper();
            adminUser.NormalizedEmail = adminUser.Email.ToUpper();



            List<ApplicationUser> users = new List<ApplicationUser>() {
                adminUser,
            };

            builder.Entity<ApplicationUser>().HasData(users);

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "admin").Id
            });


            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}
