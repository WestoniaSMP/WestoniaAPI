using System.ComponentModel.DataAnnotations.Schema;

namespace WestoniaAPI.DataLayer.Entities.Security
{
    public class WebUser : Account
    {
        public string DiscordId { get; set; } = string.Empty;

        public virtual MinecraftUser? MinecraftUser { get; set; }
    }
}
