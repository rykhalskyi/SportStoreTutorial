using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class IdentitySeedData
    {
        const string adminUser = "Admin";
        const string adminPassword = "Secret123$";

        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            var userManager = app.ApplicationServices.GetService(typeof(UserManager<IdentityUser>)) 
                as UserManager<IdentityUser>;

            var user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
