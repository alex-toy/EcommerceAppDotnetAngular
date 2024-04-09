using API.Errors;
using Core.Entities.Identities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace API.Extensions
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

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
        }

        public static void ConfigureRedis(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                string connectionString = builder.Configuration.GetConnectionString("Redis");
                ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);
                return ConnectionMultiplexer.Connect(options);
            });
        }

        public static void ConfigureIdentity(this WebApplicationBuilder builder)
        {
            IdentityBuilder identity = builder.Services.AddIdentityCore<AppUser>();

            IdentityBuilder identityBuilder = new IdentityBuilder(identity.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<AppIdentityDbContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();
        }

        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            string key = builder.Configuration["Token:Key"];
            string issuer = builder.Configuration["Token:Issuer"];

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidIssuer = issuer,
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });
        }

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
            builder.Services.AddScoped<IBasketRepo, BasketRepo>();
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
        }

        public static void ConfigureAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void ConfigureApiBehaviorOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    string[] errors = ActionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors)
                        .Select(e => e.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        public static void ConfigureCORS(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("corsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }
    }
}
