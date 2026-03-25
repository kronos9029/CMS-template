using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VietConstruction.Web.Data;
using VietConstruction.Web.Data.Entities;
using VietConstruction.Web.Options;

namespace VietConstruction.Web.Infrastructure;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(WebApplication app)
    {
        using var bootstrapScope = app.Services.CreateScope();
        var logger = bootstrapScope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseInitializer");
        var configuration = bootstrapScope.ServiceProvider.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("MySql");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            logger.LogWarning("Skipping database initialization because the MySql connection string is empty.");
            return;
        }

        var maxAttempts = Math.Max(configuration.GetValue<int?>("DatabaseInitialization:MaxAttempts") ?? 12, 1);
        var delaySeconds = Math.Max(configuration.GetValue<int?>("DatabaseInitialization:DelaySeconds") ?? 5, 1);
        Exception? lastException = null;

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var hasMigrations = dbContext.Database.GetMigrations().Any();

                if (hasMigrations)
                {
                    await dbContext.Database.MigrateAsync();
                }
                else
                {
                    await dbContext.Database.EnsureCreatedAsync();
                }

                await SeedContentAsync(dbContext);
                await SeedIdentityAsync(scope.ServiceProvider);
                logger.LogInformation("Database bootstrap completed on attempt {Attempt}/{MaxAttempts}.", attempt, maxAttempts);
                return;
            }
            catch (Exception ex)
            {
                lastException = ex;

                if (attempt == maxAttempts)
                {
                    break;
                }

                logger.LogWarning(ex,
                    "Database bootstrap attempt {Attempt}/{MaxAttempts} failed. Retrying in {DelaySeconds}s.",
                    attempt,
                    maxAttempts,
                    delaySeconds);

                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }

        logger.LogError(lastException, "Database bootstrap failed after {MaxAttempts} attempts. Application startup will stop.", maxAttempts);
        throw new InvalidOperationException("Database bootstrap failed. CMS features require a reachable MySQL database.", lastException);
    }

    private static async Task SeedIdentityAsync(IServiceProvider services)
    {
        var options = services.GetRequiredService<IOptions<CmsSeedOptions>>().Value;
        if (!options.Enabled)
        {
            return;
        }

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var roleName in new[] { CmsRoles.Admin, CmsRoles.Editor })
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        await EnsureUserAsync(userManager, options.AdminEmail, options.AdminPassword, options.AdminFullName, CmsRoles.Admin);
        await EnsureUserAsync(userManager, options.EditorEmail, options.EditorPassword, options.EditorFullName, CmsRoles.Editor);
    }

    private static async Task EnsureUserAsync(UserManager<ApplicationUser> userManager, string email, string password, string fullName, string roleName)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                EmailConfirmed = true,
                IsActive = true
            };

            var createResult = await userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", createResult.Errors.Select(x => x.Description)));
            }
        }
        else
        {
            user.FullName = fullName;
            user.IsActive = true;
            await userManager.UpdateAsync(user);
        }

        if (!await userManager.IsInRoleAsync(user, roleName))
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }

    private static async Task SeedContentAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.SiteSettings.AnyAsync())
        {
            dbContext.SiteSettings.Add(SeedContentFactory.CreateSiteSetting());
        }

        if (!await dbContext.Pages.AnyAsync())
        {
            dbContext.Pages.AddRange(SeedContentFactory.CreatePages());
        }

        if (!await dbContext.Categories.AnyAsync())
        {
            dbContext.Categories.AddRange(SeedContentFactory.CreateCategories());
        }

        await dbContext.SaveChangesAsync();

        if (!await dbContext.Projects.AnyAsync())
        {
            dbContext.Projects.AddRange(SeedContentFactory.CreateProjects());
        }

        if (!await dbContext.Posts.AnyAsync())
        {
            var categoryIds = await dbContext.Categories.ToDictionaryAsync(x => x.Slug, x => x.Id);
            dbContext.Posts.AddRange(SeedContentFactory.CreatePosts(categoryIds));
        }

        await dbContext.SaveChangesAsync();
    }
}
