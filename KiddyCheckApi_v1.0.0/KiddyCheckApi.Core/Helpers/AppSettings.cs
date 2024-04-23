
namespace KiddyCheckApi.Core.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public bool ValidateIssuer { get; set; } = true;
        public string ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public string ValidAudience { get; set; }
    }
}
