using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class CaseRecordDetails
    {
        // =====================================================
        // CASE INFO
        // =====================================================

        public int Id { get; set; }

        public string PatientName { get; set; }
            = string.Empty;

        public string Phone { get; set; }
            = string.Empty;

        public string DiseaseName { get; set; }
            = string.Empty;

        public string AddressOfPatient { get; set; }
            = string.Empty;

        public string? Notes { get; set; }

        // =====================================================
        // DATES
        // =====================================================

        public DateTime OnsetDate { get; set; }

        public DateTime DateReported { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LabConfirmedDate { get; set; }

        // =====================================================
        // STATUS
        // =====================================================

        public bool IsCommunicable { get; set; }

        public CaseStatus Status { get; set; }

        // =====================================================
        // FACILITY
        // =====================================================

        public int FacilityId { get; set; }

        public string FacilityName { get; set; }
            = string.Empty;

        // =====================================================
        // USER
        // =====================================================

        public string UserId { get; set; }
            = string.Empty;

        public string? CreatedByName { get; set; }

        // =====================================================
        // SYMPTOMS
        // =====================================================

        public List<string> Symptoms { get; set; }
            = new();

        // =====================================================
        // SAMPLES
        // =====================================================

        public List<CaseSampleDetails> Samples { get; set; }
            = new();

        // =====================================================
        // LAB TESTS
        // =====================================================

        public List<CaseLabTestDetails> LabTests { get; set; }
            = new();
    }

   

   
}