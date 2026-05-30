using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class LabResult
    {
        public Guid Id { get; set; }

        public Guid CaseRecordLabTestId { get; set; }

        public CaseRecordLabTest CaseRecordLabTest { get; set; }

        public string ResultValue { get; set; } = string.Empty;

        public LabResultStatus ResultStatus { get; set; }

        public DateTime EnteredAt { get; set; }

        public string? EnteredByUserId { get; set; }

        public bool IsVerified { get; set; }

        public string? VerifiedByUserId { get; set; }

        public string? Remarks { get; set; }

        public string? ReportLink { get; set; }

      //  public DateTime? ReportLinkExpiresAt { get; set; }


    }

}

// labResult.ReportLink = $"/lab-report/{labResult.Id}";
//labResult.ReportLinkExpiresAt = DateTime.UtcNow.AddHours(72);