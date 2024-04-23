

namespace KiddyCheckApi.Core.Helpers
{
    public static class HoraHelper
    {
        public static DateTime GetHora(string culture)
        {
            if (culture == "mx")
                return DateTime.UtcNow.AddHours(-6);

            return DateTime.UtcNow;
        }
    }
}
