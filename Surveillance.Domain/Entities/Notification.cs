

using Surveillance.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Surveillance.Domain.Entities
{
   

    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DiseaseName { get; set; } = "";

        public bool IsChecked { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public NotificationType Type { get; set; } = NotificationType.NewCase;

        // 🔗 FK → CaseRecord
        public int CaseRecordId { get; set; }
        public CaseRecord CaseRecord { get; set; } = default!;

        // 🔗 FK → Facility
        public int FacilityId { get; set; }
        public Facility Facility { get; set; } = default!;


        public Guid? LabResultId { get; set; }


        public ICollection<NotificationRecipient> Recipients { get; set; }
    = new List<NotificationRecipient>();
    }
}
























//namespace UPHC.SurveillanceDashboard.Models
//{

//    public enum NotificationType
//    {
//        NewCase=1,
//        ConfirmedPositive=2,
//        ConfirmedNegetive=3
//    }

//    public class Notification
//    {
//        public int Id { get; set; }

//        public string DiseaseName { get; set; } = "";

//        public bool IsChecked { get; set; }

//        public DateTime Timestamp { get; set; } = DateTime.Now;

//        public NotificationType Type { get; set; } = NotificationType.NewCase;


//        // 🔗 FK → CaseRecord
//        public int CaseRecordId { get; set; }
//        public CaseRecord? CaseRecord { get; set; }

//        public int FacilityId { get; set; }
//        public Facility? Facility { get; set; }
//    }

//}
