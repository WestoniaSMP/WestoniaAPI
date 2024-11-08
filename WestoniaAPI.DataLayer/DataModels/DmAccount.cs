using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestoniaAPI.DataLayer.DataModels
{
    public class DmAccount
    {
        public string Language { get; set; } = "en";
        public bool HasAcceptedGTCs { get; set; } = false;
    }
}
