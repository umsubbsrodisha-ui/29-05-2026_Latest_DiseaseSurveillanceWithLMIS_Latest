namespace Surveillance.Domain.Entities
{
    public class NotificationRecipient
    {
        public int Id { get; set; }

        public int NotificationId { get; set; }

        public Notification Notification { get; set; } = default!;

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = default!;

        public bool IsRead { get; set; } = false;

        public DateTime? ReadAt { get; set; }
    }
}