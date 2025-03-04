using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WestoniaAPI.Core;
using WestoniaAPI.DataLayer.Entities.Security;
using WestoniaAPI.DataLogic;
using WestoniaAPI.Helpers;

namespace WestoniaAPI.Controllers.v1.Security
{
    /// <summary>
    /// AuthController for handling authentication requests for the WestoniaAPI.
    /// </summary>
    /// <param name="mapper"> The AutoMapper instance. </param>
    /// <param name="authService"> The IAuthService instance. </param>
    /// <param name="logger"> The ILogger instance. </param>
    /// <param name="configuration"> The IConfiguration instance. </param>
    /// <param name="userLogic"> The IUserManager instance. </param>
    /// <param name="signInManager"> The SignInManager instance. </param>
    public class AuthController(IMapper mapper, IAuthService authService, ILogger<AuthController> logger, IConfiguration configuration, IUserLogic userLogic, SignInManager<WestoniaUser> signInManager) : WestoniaAPIBaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthService _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserLogic _userLogic = userLogic;
        private readonly SignInManager<WestoniaUser> _signInManager = signInManager;

        [HttpGet("GetToken")]
        [Authorize(AuthenticationSchemes = "Discord")]
        public async Task<IActionResult> GetToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity ?? throw new ArgumentNullException("User identity not found");

            WestoniaUser? user = await _userLogic.UserManager.FindByDiscordIdAsync(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (user == null)
                return BadRequest("User not found");

            string authToken = _authService.GenerateJwtToken(user);

            return Ok(new { Id = user.Id, DiscordId = user.DiscordId, Token = authToken });
        }
    }
}
