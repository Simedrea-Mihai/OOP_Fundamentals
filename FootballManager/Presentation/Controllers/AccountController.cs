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
        private readonly IEmailSenderPaperCut _emailSender;

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }


        public AccountController(IAuthenticationService authenticationService,
            UserManager<ApplicationUser> userManager,
            IEmailSender sender,
            IEmailSenderPaperCut emailSender)
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
            _sender = sender;
            _emailSender = emailSender;
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
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync([FromBody] RegistrationRequest request)
        {
            return Ok(await _authenticationService.RegisterAsync(request));
        }
        */

        public string EmailConfirmationUrl { get; set; }

        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Registers a user",
            Description = "Registers a user",
            OperationId = "auth.register",
            Tags = new[] { "AuthEndpoints" })
        ]
        public async Task<ActionResult<RegistrationResponse>> OnGetAsync([FromQuery] RegistrationRequest request)
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
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    EmailConfirmationUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = "" },
                        protocol: Request.Scheme);

                    Console.WriteLine(code);

                    //await sender.SendEmailAsync(request.Email, "Confirmation", EmailConfirmationUrl);
                    _emailSender.SendEmail(user.Email, "Confirm your email", "Please confirm your account by clicking this link: " + EmailConfirmationUrl);

                    return new RegistrationResponse { UserId = user.Id };
                }

                throw new Exception($"{result.Errors}");
            }

            throw new Exception($"Email {request.Email} already exists.");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return Ok("ConfirmEmail");
            return NoContent();

        }


    }



}