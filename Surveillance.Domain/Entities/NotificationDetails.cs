using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class NotificationDetails
    {
        // Notification
        public int Id { get; set; }

        public int FacilityId { get; set; }

        public int CaseRecordId { get; set; }

        public string Type { get; set; } = string.Empty;

        public bool IsChecked { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Facility
        public string FacilityName { get; set; } = string.Empty;

        // CaseRecord
        public string DiseaseName { get; set; } = string.Empty;

        public CaseStatus Status { get; set; }

        public Guid? LabResultId { get; set; }
    }
}
