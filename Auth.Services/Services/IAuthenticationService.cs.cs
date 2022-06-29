using Auth.Services.Entities;

namespace Auth.Services.Services
{
    public interface IAuthenticationService
    {
        Task<Response> LogInAsync(UserCredentials userCredentials);
        Task<Response> RegisterAsync(RegisterModel user);
    }
}
