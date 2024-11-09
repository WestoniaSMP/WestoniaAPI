using System.ComponentModel.DataAnnotations.Schema;

namespace WestoniaAPI.DataLayer.Entities.Security
{
    public class MinecraftUser : Account
    {
        public Guid MinecraftUuid { get; set; } = Guid.Empty;

        public DateTime FirstJoin { get; set; } = DateTime.Now;

        public DateTime LastJoin { get; set; } = DateTime.Now;

        public long? WebUserId { get; set; }

        [ForeignKey(nameof(WebUserId))]
        public virtual WebUser? WebUser { get; set; }
    }
}
