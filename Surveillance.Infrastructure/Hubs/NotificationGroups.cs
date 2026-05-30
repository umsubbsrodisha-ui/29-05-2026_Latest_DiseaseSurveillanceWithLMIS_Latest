namespace Surveillance.Infrastructure.Hubs
{
    public static class NotificationGroups
    {
        public const string Analysts = "Analysts";

        public const string Dashboard = "Dashboard";

        public static string LT(int facilityId)
            => $"LT_{facilityId}";

        public static string MB(int facilityId)
            => $"MB_{facilityId}";

        public static string MO(int facilityId)
            => $"MO_{facilityId}";
    }
}