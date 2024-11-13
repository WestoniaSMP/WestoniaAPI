using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.Core
{
    public interface IAuthService
    {
        string GenerateJwtToken(WestoniaUser user);
    }
}
