using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public static class WebApplicationBuilderExtension
    {
        public static void ConfigureDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
        }

        //public static void ConfigureIdentity(this WebApplicationBuilder builder)
        //{
        //    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        //        .AddEntityFrameworkStores<DatabaseContext>()
        //        .AddDefaultTokenProviders();
        //}

        //public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        //{
        //    builder.Services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    });
        //}

        //public static void ConfigureJwtBearer(this WebApplicationBuilder builder)
        //{
        //    builder.Services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(options =>
        //    {
        //        options.SaveToken = true;
        //        options.RequireHttpsMetadata = false;
        //        options.TokenValidationParameters = new TokenValidationParameters()
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidAudience = builder.Configuration["JWT:ValidAudience"],
        //            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        //            ClockSkew = TimeSpan.Zero,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        //        };
        //    });
        //}

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
