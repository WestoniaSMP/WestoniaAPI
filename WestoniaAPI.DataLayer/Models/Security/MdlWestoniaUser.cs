using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestoniaAPI.DataLayer.Models.Security
{
    public class MdlWestoniaUser
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
