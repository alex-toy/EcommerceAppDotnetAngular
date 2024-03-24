using API;
using API.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.ConfigureDbContext();
//builder.ConfigureIdentity();
//builder.ConfigureAuthentication();
//builder.ConfigureJwtBearer();
builder.ConfigureServices();
builder.ConfigureAutoMapper();
builder.ConfigureApiBehaviorOptions();

builder.Services.AddSwaggerGen();






WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

await app.SeedDatabaseAsync();

app.Run();
