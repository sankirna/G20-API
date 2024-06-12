using G20.API.Factories.Categories;
using G20.API.Factories.Cities;
using G20.API.Factories.Countries;
using G20.API.Factories.Coupons;
using G20.API.Factories.Media;
using G20.API.Factories.Roles;
using G20.API.Factories.TicketCategory;
using G20.API.Factories.States;
using G20.API.Factories.SubCategories;
using G20.API.Factories.Teams;
using G20.API.Factories.Users;
using G20.API.Factories.Venue;
using G20.Core.DbContexts;
using G20.Core.IndentityModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nop.Core.Infrastructure;
using System.Text;
using G20.API.Factories.VenueTicketCategoriesMap;

namespace G20.API.Infrastructure
{
    public class WebAPIStartUp : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                                                   options.UseSqlServer(
                                                       configuration.GetConnectionString("ConnectionString")));
                                                        services.AddIdentity<ApplicationUser, ApplicationRole>(
                                                                options =>
                                                                {
                                                                    options.SignIn.RequireConfirmedAccount = false;
                                                                    //Other options go here
                                                                })
                   .AddEntityFrameworkStores<DatabaseContext>();

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SymmetricSecurityKey"]))
                };
            });

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            services.AddProblemDetails();
            services.AddControllers();
            //builder.Services.AddRouting(options => options.LowercaseUrls = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                      policy =>
                                      {
                                          policy.AllowAnyOrigin()
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod();
                                      });
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Factoreis
            services.AddScoped<IMediaFactoryModel, MediaFactoryModel>();
            services.AddScoped<ICountryFactoryModel, CountryFactoryModel>();
            services.AddScoped<IStateFactoryModel, StateFactoryModel>();
            services.AddScoped<ICityFactoryModel, CityFactoryModel>();
            services.AddScoped<ICouponFactoryModel, CouponFactoryModel>();
            services.AddScoped<ICategoryFactoryModel, CategoryFactoryModel>();
            services.AddScoped<ISubCategoryFactoryModel, SubCategoryFactoryModel>();
            services.AddScoped<IVenueFactoryModel, VenueFactoryModel>();
            services.AddScoped<ITeamFactoryModel, TeamFactoryModel>();
            services.AddScoped<ITicketCategoryFactoryModel, TicketCategoryFactoryModel>();
            services.AddScoped<IVenueTicketCategoryMapFactoryModel, VenueTicketCategoryMapFactoryModel>();
            //User Management
            services.AddScoped<IUserFactoryModel, UserFactoryModel>();
            services.AddScoped<IRoleFactoryModel, RoleFactoryModel>();
        }

        public void Configure(IApplicationBuilder application)
        {

        }

        public int Order => 100;

    }
}
