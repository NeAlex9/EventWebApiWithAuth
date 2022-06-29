using System.Text.Json.Serialization;
using Auth.Service.Identity.Context;
using Auth.Service.Identity.Entities;
using Auth.Service.Identity.Services;
using Auth.Services.Entities;
using Auth.Services.Services;
using Events.Services.EntityFramework.Context;
using Events.Services.EntityFramework.Services;
using Events.Services.Services;
using EventsWebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using EventsWebApi.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

builder.Services
    .AddDbContext<AuthenticationContext>(opt => opt.UseSqlServer(connectionString))
    .AddDbContext<EventContext>(opt => opt.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 1;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.SignIn.RequireConfirmedAccount = false;

    })
    .AddEntityFrameworkStores<AuthenticationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

IConfigurationSection settingsSection = builder.Configuration.GetSection("AppSettings");
AppSettings settings = settingsSection.Get<AppSettings>();

builder.Services
    .AddAuthentication(settings);

builder.Services
    .Configure<AppSettings>(settingsSection)
    .AddTransient<IEventService, EventService>()
    .AddTransient<IAuthenticationService, AuthenticationService>()
    .AddAutoMapper(typeof(EventService).Assembly, typeof(EventController).Assembly);

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddJsonOptions(option => option.JsonSerializerOptions.WriteIndented = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
