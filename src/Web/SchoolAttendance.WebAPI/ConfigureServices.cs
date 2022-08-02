using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Infrastructure.Data;
using SchoolAttendance.WebAPI.Filters;
using SchoolAttendance.WebAPI.Infrastructure.Services;
using System.Text;

namespace SchoolAttendance.WebAPI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<SchoolAttendanceContext>();

            services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

            services.AddRazorPages();

            var allowedOrigins = new List<string>();
            var allowOrigins = configuration["AllowedOrigins"].Split(",");

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(allowOrigins)
                    //.SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            //Enable Swagger Documentation
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "School Attendance API",
                    Version = "v1",
                    Description ="The Web API for School Attendance System",
                    TermsOfService = new Uri("https://example.com/terms")
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                    }
                });
            });

            //Enable JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Tokens:Issuer"],
                    ValidAudiences = new List<string>
                    {
                          "mobileapp","webapp"
                    },

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
