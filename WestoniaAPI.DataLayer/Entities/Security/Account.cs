using Microsoft.AspNetCore.Identity;

namespace WestoniaAPI.DataLayer.Entities.Security
{
    public class Account : IdentityUser<long>
    {
        public string Language { get; set; } = "en";
        public bool HasAcceptedGTCs { get; set; } = false;
    }
}
