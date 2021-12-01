using System.Threading.Tasks;
using Application.Contracts.Identity;
using Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
            Summary = "Authenticates a user",
            Description = "Authenticates a user",
            OperationId = "auth.authenticate",
            Tags = new[] { "AuthEndpoints" })
        ]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync([FromQuery] AuthenticationRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Registers a user",
            Description = "Registers a user",
            OperationId = "auth.register",
            Tags = new[] { "AuthEndpoints" })
        ]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync([FromQuery] RegistrationRequest request)
        {
            return Ok(await _authenticationService.RegisterAsync(request));
        }
    }



}