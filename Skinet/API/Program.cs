using API.Extensions;
using API.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.ConfigureDbContext();
builder.ConfigureRedis();
builder.ConfigureIdentity();
builder.ConfigureAuthentication();
builder.ConfigureServices();
builder.ConfigureAutoMapper();
builder.ConfigureApiBehaviorOptions();

builder.Services.AddSwaggerGen();

builder.ConfigureCORS();






WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseStaticFiles();

app.UseCors("corsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.SeedDatabaseAsync();

app.Run();
