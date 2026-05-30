using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class CaseLabTestDto
    {
        // =====================================================
        // TEST INFO
        // =====================================================

        public Guid CaseRecordLabTestId { get; set; }

        public string LabTestName { get; set; }
            = string.Empty;

        // =====================================================
        // STATUS
        // =====================================================

        public string LabResultStatus { get; set; }
            = string.Empty;

        // =====================================================
        // TEST DATES
        // =====================================================

        public DateTime? TestedAt { get; set; }

        // =====================================================
        // RESULT
        // =====================================================

        public string? ResultValue { get; set; }

        public string? Remarks { get; set; }

        // =====================================================
        // REPORT
        // =====================================================

        public string? ReportPath { get; set; }

        // =====================================================
        // SAMPLE LINK
        // =====================================================

        public Guid SampleId { get; set; }

        public string? SampleBarcode { get; set; }



        public Guid? LabResultId { get; set; }

        public bool IsVerified { get; set; }
    }
}