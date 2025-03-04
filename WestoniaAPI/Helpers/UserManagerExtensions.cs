using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.Helpers
{
    public static class UserManagerExtensions
    {
        public static async Task<WestoniaUser?> FindByDiscordIdAsync(this UserManager<WestoniaUser> userManager, string discordId)
        {
            return await userManager.Users.FirstOrDefaultAsync(u => u.DiscordId == discordId);
        }
    }
}
