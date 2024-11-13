using Microsoft.AspNetCore.Identity;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.Core
{
    public interface IUserLogic
    {
        /// <summary>
        /// Creates a new Westonia user.
        /// </summary>
        /// <param name="userToCreate"> The user to create. </param>
        /// <returns> The identity result of the creation. </returns>
        Task<IdentityResult> CreateUser(WestoniaUser userToCreate);

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
