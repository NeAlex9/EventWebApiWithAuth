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
        public async Task<ActionResult<Response>> LogIn([FromBody] UserCredentials userCredentials)
        {
            var response = await _authenticationService.LogInAsync(userCredentials);
            return response.Success ? response : BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Response>> Register([FromBody] RegisterModel user)
        {
            var response = await _authenticationService.RegisterAsync(user);
            return response.Success ? response : BadRequest(response);
        }
    }
}
