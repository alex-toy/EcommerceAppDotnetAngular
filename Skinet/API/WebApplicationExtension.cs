using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public static class WebApplicationExtension
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            StoreContext context = services.GetRequiredService<StoreContext>();
            ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                await context.Database.MigrateAsync();

                await StoreContextSeed.SeedDataAsync(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
