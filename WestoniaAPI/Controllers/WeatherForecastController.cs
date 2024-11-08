using AutoMapper;
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
    public class WeatherForecastController(IMapper mapper, IAuthService authService, ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthService _authService = authService;
        private readonly ILogger<WeatherForecastController> _logger = logger;


        [HttpPost]
        public GlobalResultModel<MdlMinecraftUser> Test()
        {
            MinecraftUser user = new MinecraftUser();
            user.UserName = "Test";
            user.MinecraftUuid = new Guid();

            _authService.GenerateToken(user);

            DmMinecraftUser dmUser = _mapper.Map<MinecraftUser, DmMinecraftUser>(user);
            MdlMinecraftUser mdlUser = _mapper.Map<DmMinecraftUser, MdlMinecraftUser>(dmUser);

            if (mdlUser is null)
                return new GlobalResultModel<MdlMinecraftUser>(false, "Failed to map user.");

            return new GlobalResultModel<MdlMinecraftUser>(mdlUser, "User mapped successfully.");
        }
    }
}
