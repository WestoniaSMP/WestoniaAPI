using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestoniaAPI.DataLayer.Models
{
    public class MdlAccount
    {
        public string Language { get; set; } = "en";
        public bool HasAcceptedGTCs { get; set; } = false;
    }
}
