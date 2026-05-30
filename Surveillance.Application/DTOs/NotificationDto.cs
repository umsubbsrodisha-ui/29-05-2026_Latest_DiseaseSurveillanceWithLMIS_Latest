using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public string DiseaseName { get; set; } = string.Empty;

        public bool IsChecked { get; set; }

        public DateTime Timestamp { get; set; }

        public string Type { get; set; } = string.Empty;

        public int CaseRecordId { get; set; }

        public int FacilityId { get; set; }

        public string FacilityName { get; set; } = string.Empty;
        
        public Guid? LabResultId { get; set; }
    }
}
