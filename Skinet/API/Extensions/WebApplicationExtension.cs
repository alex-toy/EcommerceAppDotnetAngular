using Core.Entities.Identities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class WebApplicationExtension
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            StoreContext context = services.GetRequiredService<StoreContext>();
            AppIdentityDbContext identityContext = services.GetRequiredService<AppIdentityDbContext>();
            UserManager<AppUser> userManager = services.GetRequiredService<UserManager<AppUser>>();

            ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                await context.Database.MigrateAsync();
                await identityContext.Database.MigrateAsync();

                await StoreContextSeed.SeedDataAsync(context);
                await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
