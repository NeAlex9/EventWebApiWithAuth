﻿using System.Text;
using Auth.Services.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EventsWebApi.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services,
            AppSettings settings)
        {
            byte[] signingKey = Encoding.UTF8.GetBytes(settings.EncryptionKey);
            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidAudience = settings.ValidAudience,
                        ValidIssuer = settings.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                        ValidateLifetime = true,
                        LifetimeValidator = LifetimeValidator
                    };
                });

            return services;
        }

        private static bool LifetimeValidator(DateTime? notBefore,
            DateTime? expires,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters)
        {
            return expires != null && expires > DateTime.UtcNow;
        }
    }
}
