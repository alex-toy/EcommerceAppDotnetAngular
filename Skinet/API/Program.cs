using API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.ConfigureDbContext();
//builder.ConfigureIdentity();
//builder.ConfigureAuthentication();
//builder.ConfigureJwtBearer();
//builder.ConfigureServices();





var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
