using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WestoniaAPI.DataLayer.Entities.Security
{
    public class WestoniaUser : IdentityUser<long>
    {
        public string Language { get; set; } = "en";

        public bool HasAcceptedGTCs { get; set; } = false;

        public Guid MinecraftUuid { get; set; } = Guid.Empty;

        public string? MinecraftUsername { get; set; }

        public string DiscordId { get; set; } = string.Empty;

        public string? DiscordUsername { get; set; }

        public DateTime FirstJoin { get; set; } = DateTime.Now;

        public DateTime LastJoin { get; set; } = DateTime.Now;
    }
}
