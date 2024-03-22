using API;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.ConfigureDbContext();
//builder.ConfigureIdentity();
//builder.ConfigureAuthentication();
//builder.ConfigureJwtBearer();
builder.ConfigureServices();





WebApplication app = builder.Build();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

await app.SeedDatabaseAsync();

app.Run();
