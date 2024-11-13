using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WestoniaAPI.Core;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.DataLogic
{
    public class UserLogic(UserManager<WestoniaUser> userManager, RoleManager<WestoniaRole> roleManager) : IUserLogic
    {
        private readonly UserManager<WestoniaUser> _userManager = userManager;
        private readonly RoleManager<WestoniaRole> _roleManager = roleManager;

        /// <inheritdoc />
        public async Task<IdentityResult> CreateUser(WestoniaUser westoniaUser)
        {
            WestoniaUser newUser = new WestoniaUser
            {
                UserName = "TestUser",
                Email = "test@mail.de",
                EmailConfirmed = true,
                FirstJoin = DateTime.Now,
                LastJoin = DateTime.Now,
                HasAcceptedGTCs = true,
                Language = "en",
                DiscordId = "TestId",
                MinecraftUuid = Guid.NewGuid()
            };

            return await _userManager.CreateAsync(newUser);
        }

        /// <inheritdoc />
        public async Task<bool> UserExists(string discordId)
        {
            WestoniaUser? foundUser = await _userManager.Users.FirstOrDefaultAsync(u => u.DiscordId == discordId);
            return foundUser != null;
        } 

        /// <inheritdoc />
        public async Task<bool> UserExists(Guid minecraftUuid)
        {
            WestoniaUser? foundUser = await _userManager.Users.FirstOrDefaultAsync(u => u.MinecraftUuid == minecraftUuid);
            return foundUser != null;
        }
    }
}
