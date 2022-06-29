using Auth.Services.Entities;
using Auth.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LogIn([FromBody] UserCredentials userCredentials)
        {
            var token = await _authenticationService.LogInAsync(userCredentials);
            return token is null ? BadRequest() : token;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterModel user)
        {
            var token = await _authenticationService.RegisterAsync(user);
            return token is null ? BadRequest() : token;
        }
    }
}
