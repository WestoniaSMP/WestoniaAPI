using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WestoniaAPI.Core;
using WestoniaAPI.DataLayer.DataModels;
using WestoniaAPI.DataLayer.Entities.Security;
using WestoniaAPI.DataLayer.Models;
using WestoniaAPI.DataLogic;

namespace WestoniaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IMapper mapper, IAuthService authService, ILogger<WeatherForecastController> logger, IUserLogic accountLogic) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthService _authService = authService;
        private readonly ILogger<WeatherForecastController> _logger = logger;
        private readonly IUserLogic _accountLogic = accountLogic;


        [HttpPost]
        [AllowAnonymous]
        public async Task<GlobalResultModel<MdlWestoniaUser>> Test()
        {
            var contextUser = HttpContext.User;
            WestoniaUser user = new WestoniaUser();
            user.UserName = "Test";
            user.MinecraftUuid = new Guid();

            bool userExists = await _accountLogic.UserExists("TestId");

            if (!userExists)
            {
                IdentityResult createResult = await _accountLogic.CreateUser(user);
                if (!createResult.Succeeded)
                {
                    return new GlobalResultModel<MdlWestoniaUser>(false, "Failed to create user. Errors: " + string.Join(", ", createResult.Errors));
                }
            }

            string kevin = _authService.GenerateJwtToken(user);

            DmWestoniaUser dmUser = _mapper.Map<WestoniaUser, DmWestoniaUser>(user);
            MdlWestoniaUser mdlUser = _mapper.Map<DmWestoniaUser, MdlWestoniaUser>(dmUser);

            if (mdlUser is null)
                return new GlobalResultModel<MdlWestoniaUser>(false, "Failed to map user.");

            return new GlobalResultModel<MdlWestoniaUser>(mdlUser, "User mapped successfully.");
        }
    }
}
