using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestoniaAPI.DataLayer.Models
{
    public class MdlMinecraftUser : MdlAccount
    {
        public Guid MinecraftUuid { get; set; } = Guid.Empty;

        public DateTime FirstJoin { get; set; } = DateTime.Now;

        public DateTime LastJoin { get; set; } = DateTime.Now;
    }
}
