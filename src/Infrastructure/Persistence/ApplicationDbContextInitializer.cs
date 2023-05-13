using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Common.Helpers;
using Microsoft.Extensions.Logging;
using ProFilePOC2.Application.Common.Models.Dtos;
using ProFilePOC2.Domain.Entities;

namespace ProFilePOC2.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    // private readonly UserManager<ApplicationUser> _userManager;
    // private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context
        // , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
        )
    {
        _logger = logger;
        _context = context;
        // _userManager = userManager;
        // _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new Role { Name = "Administrator" };

        if (_context.Roles.All(r => !r.Name.Equals(administratorRole.Name)))
        {
            await _context.Roles.AddAsync(administratorRole);
        }

        // Default users
        var administrator = new User
        {
            Username = "administrator@profile", 
            Email = "administrator@profile",
        };

        if (_context.Users.All(u => !u.Username.Equals(administrator.Username)))
        {
            administrator.PasswordHash = SecurityUtil.Hash("Administrator1!");
            await _context.Users.AddAsync(administrator);
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                var role = new Role { Name = "Administrator" };
                administrator.Role = role;
                _context.Users.Update(administrator);
            }
        }
    }
}
