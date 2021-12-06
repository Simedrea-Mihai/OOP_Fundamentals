using System;
using System.Text;
using System.Threading.Tasks;

using Application.Contracts.Identity;
using Application.Models.Authentication;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _sender;
        private readonly EmailSender sender = new EmailSender();

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public AccountController(IAuthenticationService authenticationService, UserManager<ApplicationUser> userManager, IEmailSender sender)
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
            _sender = sender;
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
        /*

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
        }*/

        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Registers a user",
            Description = "Registers a user",
            OperationId = "auth.register",
            Tags = new[] { "AuthEndpoints" })
        ]
        public async Task<ActionResult<RegistrationResponse>> OnGetAsync([FromQuery] RegistrationRequest request, string email, string returnUrl = null)
        {

            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null) throw new Exception($"Username '{request.UserName}' already exists.");


            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                BirthDate = request.BirthDate,
                EmailConfirmed = false
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);

                await sender.SendEmailAsync(request.Email, "Confirmation", EmailConfirmationUrl);

                var confirmedEmail = await _userManager.IsEmailConfirmedAsync(user);
                if (confirmedEmail == true)
                    user.EmailConfirmed = true;

                await _userManager.ConfirmEmailAsync(user, code);

                if (user.EmailConfirmed)
                {
                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (result.Succeeded)
                    {
                        return new RegistrationResponse { UserId = user.Id };
                    }
                }
                else
                    return Ok(EmailConfirmationUrl);



                /*
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    //return new RegistrationResponse { UserId = user.Id };
                }

                throw new Exception($"{result.Errors}");*/
            }

            //throw new Exception($"Email {request.Email} already exists.");
            throw new Exception("Confirm your email");
        }

    }



}