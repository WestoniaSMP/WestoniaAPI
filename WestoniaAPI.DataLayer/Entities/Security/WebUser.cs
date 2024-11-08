namespace WestoniaAPI.DataLayer.Entities.Security
{
    public class WebUser : Account
    {
        public string DiscordId { get; set; } = string.Empty;
    }
}
