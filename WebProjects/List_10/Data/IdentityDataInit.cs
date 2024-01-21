using Microsoft.AspNetCore.Identity;

namespace List_10.Data
{
    public class IdentityDataInit
    {
        public static void SeedData(UserManager<IdentityUser> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUser(userManager);
        }

        public static void SeedRoles (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "Admin"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedOneUser(UserManager<IdentityUser> userManager,
            string username, string password, string role = null)
        { 
            if (userManager.FindByNameAsync(username).Result == null)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = username,
                    Email = username
                };
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded && role != null)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }

        public static void SeedUser (UserManager<IdentityUser> userManager)
        {
            SeedOneUser(userManager, "customer@customer.com", "customer");
            SeedOneUser(userManager, "admin@administrator.com", "admin", "Admin");
        }
    }
}
