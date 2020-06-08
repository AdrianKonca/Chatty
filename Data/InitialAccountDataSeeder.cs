using System.Collections.Generic;
using Chatty.Models;
using Microsoft.AspNetCore.Identity;

namespace Chatty.Data
{
    public class InitialAccountDataSeeder
    {
        private static string RootName = "root@localhost";
        private static string RootPassword = "u/&S!3E7rRFPT3Vh";
        private static string RootEmail = "root@localhost";
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        public static void SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            var rolesNames = new List<string>() { "User", "Admin", "Root" };
            foreach (var roleName in rolesNames)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    IdentityRole<int> role = new IdentityRole<int>(roleName);
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }

            }
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync(RootName).Result == null)
            {
                User user = new User();
                user.UserName = RootName;
                user.Email = RootEmail;
                user.EmailConfirmed = true;
                
                IdentityResult result = userManager.CreateAsync(user, RootPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user, "Root").Wait();
                }
            }
        }

    }
}