using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Service.Identity.Entities;
using Auth.Services.Entities;
using Auth.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Service.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> options)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _appSettings = options.Value;
        }

        public async Task<Response> LogInAsync(UserCredentials userCredentials)
        {
            ArgumentNullException.ThrowIfNull(nameof(userCredentials));
            var user = await _userManager.FindByNameAsync(userCredentials.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, userCredentials.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var authClaims = GenerateClaims(roles);
                var token = GetToken(authClaims);
                return new Response(true, "User authorized successfully!", token);
            }

            return new Response("Invalid login or password");
        }

        public async Task<Response> RegisterAsync(RegisterModel user)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));
            var userExists = await _userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
            {
                return new Response("There are user with the same login");
            }
            user.Roles = user.Roles.Select(role => role.ToLower());
            ApplicationUser applicationUser = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = user.UserName
            };

            var result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!result.Succeeded)
            {
                return new Response("Cannot register user");
            }

            if (!await ValidateRoles(user.Roles))
            {
                return new Response("Invalid role");
            }

            await _userManager.AddToRolesAsync(applicationUser, user.Roles);
            var authClaims = GenerateClaims(user.Roles);
            var token = GetToken(authClaims);

            return new Response(true, "User registered successfully!", token);
        }

        private async Task<bool> ValidateRoles(IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return false;
                }
            }

            return true;
        }

        private string GetToken(IEnumerable<Claim> claims)
        {
            const int expiringDays = 7;
            byte[] securityKey = Encoding.UTF8.GetBytes(_appSettings.EncryptionKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(securityKey);

            var securityToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(expiringDays),
                claims: claims,
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
                issuer: _appSettings.ValidIssuer,
                audience: _appSettings.ValidAudience
            );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        private List<Claim> GenerateClaims(IEnumerable<string> roles)
        {
            var authClaims = new List<Claim>();
            foreach (var userRole in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }
    }
}
