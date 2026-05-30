using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    // =========================================================
    // LAB TEST DETAILS
    // =========================================================

    public class CaseLabTestDetails
    {
        public Guid CaseRecordLabTestId { get; set; }

        public string LabTestName { get; set; }
            = string.Empty;

        public LabResultStatus LabResultStatus { get; set; }

        public DateTime? TestedAt { get; set; }

        public string? ResultValue { get; set; }

        public string? Remarks { get; set; }

        public string? ReportPath { get; set; }

        public Guid SampleId { get; set; }

        public string? SampleBarcode { get; set; }

        public Guid? LabResultId { get; set; }

        public bool IsVerified { get; set; }
    }
}
