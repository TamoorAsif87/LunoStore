using Identity.Account.Models;

namespace Identity.Data.Seed;

public class IdentityDBSeed : IDataSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IdentityContext _context;

    public IdentityDBSeed(IdentityContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedAllAsync()
    {
        if (!_context.Database.GetPendingMigrations().Any())
        {
            if (!_context.ApplicationUsers.Any())
            {
                foreach (var role in IdentityInitialData.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role.Name!))
                    {
                        await _roleManager.CreateAsync(role);
                    }
                }
                foreach (var user in IdentityInitialData.Users)
                {
                    var existingUser = await _userManager.FindByEmailAsync(user.Email!);
                    if (existingUser == null)
                    {
                       var result =   await _userManager.CreateAsync(user, IdentityInitialData.UserPasswords[user.Email!]);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, IdentityInitialData.UserRoles[user.Email!]);
                        }

                      
                    }
                }


            }
        }
    }
}
