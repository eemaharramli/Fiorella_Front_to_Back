using System.Collections.Generic;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Data
{
    public class DataInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DataInitializer(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this._dbContext = dbContext;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        
        public async Task SeedDataAsync()
        {
            await this._dbContext.Database.MigrateAsync();

            var roles = new List<string>
            {
                RoleConstants.AdminRole,
                RoleConstants.ModeratorRole,
                RoleConstants.UserRole
            };

            foreach (var role in roles)
            {
                if (await this._roleManager.RoleExistsAsync(role))
                {
                    continue;
                }

                await this._roleManager.CreateAsync(new IdentityRole(role));
            }

            var user = new User
            {
                Fullname = "Admin",
                UserName = "Admin",
                Email = "elnur@maharramli.az"
            };

            if (await this._userManager.FindByNameAsync(user.UserName) != null)
            {
                return;
            }

            await this._userManager.CreateAsync(user, "@WSX3edc23");
            await this._userManager.AddToRoleAsync(user, RoleConstants.AdminRole);
        }
    }
}