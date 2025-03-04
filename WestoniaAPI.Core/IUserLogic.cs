using Microsoft.AspNetCore.Identity;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.Core
{
    public interface IUserLogic
    {
        UserManager<WestoniaUser> UserManager { get; }
        RoleManager<WestoniaRole> RoleManager { get; }

        /// <summary>
        /// Creates a new Westonia user.
        /// </summary>
        /// <param name="discordId"> The Discord ID of the user to create. </param>
        /// <param name="discordUserName"> The username of the users Discord identity. </param>
        /// <returns> The identity result of the creation. </returns>
        Task<IdentityResult> CreateUser(string discordId, string discordUserName);

        /// <summary>
        /// Checks if a user with the given Minecraft UUID exists.
        /// </summary>
        /// <param name="minecraftUuid"> The Minecraft UUID to check for .</param>
        /// <returns> True if a user with the given Minecraft UUID exists, false otherwise. </returns>
        Task<bool> UserExists(Guid minecraftUuid);

        /// <summary>
        /// Checks if a user with the given Discord ID exists.
        /// </summary>
        /// <param name="discordId"> The Discord ID to check for. </param>
        /// <returns> True if a user with the given Discord ID exists, false otherwise. </returns>
        Task<bool> UserExists(string discordId);
    }
}
