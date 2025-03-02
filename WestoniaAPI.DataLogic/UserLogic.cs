using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using WestoniaAPI.Core;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.DataLogic
{
    public class UserLogic(UserManager<WestoniaUser> userManager, RoleManager<WestoniaRole> roleManager) : IUserLogic
    {
        private readonly UserManager<WestoniaUser> _userManager = userManager;
        private readonly RoleManager<WestoniaRole> _roleManager = roleManager;

        public UserManager<WestoniaUser> UserManager => _userManager;
        public RoleManager<WestoniaRole> RoleManager => _roleManager;

        /// <inheritdoc />
        public async Task<IdentityResult> CreateUser(string discordId, string discordEmail, string discordUserName)
        {
            
            WestoniaUser userToBeCreated = new WestoniaUser
            {
                DiscordId = discordId,
                Email = discordEmail,
                UserName = discordUserName,
                EmailConfirmed = true,
                NormalizedEmail = discordId.ToUpper(),
                NormalizedUserName = discordUserName.ToUpper(),
            };

            return await _userManager.CreateAsync(userToBeCreated);
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
