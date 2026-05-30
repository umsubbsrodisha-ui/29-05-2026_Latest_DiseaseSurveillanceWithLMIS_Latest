

//|      Field |                      Purpose |
//| ------------------ | ----------------------------   |
//| `OnsetDate` ✅      | Symptom start (epidemiology) |
//| `DateReported`     | Facility visit               |
//| `CreatedDate`      | System entry                 |
//| `LabConfirmedDate` | Diagnosis  result out        |

//OnsetDate        → when illness/symptoms started ✅
//DateReported     → when patient reached facility
//CreatedDate      → when system recorded
//LabConfirmedDate → when diagnosis confirmed

using System.ComponentModel.DataAnnotations;
using Surveillance.Domain.Enums;

namespace Surveillance.Domain.Entities
{
    public class CaseRecord
    {
        public int Id { get; set; }

        // =====================================================
        // PATIENT INFORMATION
        // =====================================================

        [Required(ErrorMessage = "Patient name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string PatientName { get; set; } = "";

        [Required(ErrorMessage = "Phone number is mandatory")]
        [RegularExpression(@"^[6-9]\d{9}$",
            ErrorMessage = "Enter valid 10-digit mobile number")]
        public string Phone { get; set; } = "";

        [Required(ErrorMessage = "Address is required")]
        [StringLength(300,
            ErrorMessage = "Address too long (max 300 characters)")]
        public string AddressOfPatient { get; set; } = "";

        // =====================================================
        // CLINICAL INFORMATION
        // =====================================================

        /// <summary>
        /// Doctor / MO final suspected disease.
        /// Can be manually overridden.
        /// </summary>
        [StringLength(100,
            ErrorMessage = "Disease name too long")]
        public string DiseaseName { get; set; } = string.Empty;

        /// <summary>
        /// Optional clinical notes entered by doctor / DEO.
        /// </summary>
        [StringLength(1000,
            ErrorMessage = "Clinical notes too long")]
        public string? ClinicalNotes { get; set; }

        /// <summary>
        /// Temporary summary text.
        /// Actual symptoms are stored in CaseRecordSymptoms.
        /// </summary>
        [StringLength(500,
            ErrorMessage = "Symptoms summary too long")]
        public string? SymptomsSummary { get; set; }

        [Required(ErrorMessage =
            "When symptoms started must be recorded")]
        public DateTime OnsetDate { get; set; }

        [Required(ErrorMessage =
            "Patient visit date is required")]
        public DateTime DateReported { get; set; }

        public bool IsCommunicable { get; set; }

        // =====================================================
        // CASE STATUS
        // =====================================================

        /// <summary>
        /// Overall disease investigation status.
        /// Suspected -> Confirmed / Negative
        /// </summary>
        public CaseStatus Status { get; set; }
            = CaseStatus.Suspected;

        public DateTime CreatedDate { get; set; }
            = DateTime.UtcNow;

        public DateTime? LabConfirmedDate { get; set; }

        // =====================================================
        // REPORT MANAGEMENT
        // =====================================================

        /// <summary>
        /// Final generated report path/url.
        /// </summary>
        public string? FinalReportPath { get; set; }

        /// <summary>
        /// Report link expiry (72 hours etc.)
        /// </summary>
        public DateTime? ReportExpiryDate { get; set; }

        // =====================================================
        // USER / FACILITY
        // =====================================================

        [Required]
        public string UserId { get; set; } = "";

        public ApplicationUser User { get; set; }
            = default!;

        [Required(ErrorMessage ="Medical Facility is required")]
        public int FacilityId { get; set; }

        public Facility Facility { get; set; }= default!;

        // =====================================================
        // NOTIFICATIONS
        // =====================================================

        public ICollection<Notification> Notifications
        { get; set; }
            = new List<Notification>();

        // =====================================================
        // MANY TO MANY RELATIONS
        // =====================================================

        /// <summary>
        /// Selected symptoms for this case.
        /// </summary>
        public ICollection<CaseRecordSymptom> CaseRecordSymptoms
        { get; set; }
            = new List<CaseRecordSymptom>();

        /// <summary>
        /// Recommended / selected lab tests.
        /// </summary>
        public ICollection<CaseRecordLabTest> LabTests
        { get; set; }
            = new List<CaseRecordLabTest>();

        // =====================================================
        // SAMPLE / LMIS
        // =====================================================

        /// <summary>
        /// Multiple samples can belong to one case.
        /// Example:
        /// Blood + Serum + Swab
        /// </summary>
        public ICollection<Sample> Samples
        { get; set; }
            = new List<Sample>();
    }
}

























//using Surveillance.Domain.Enums;
//using System.ComponentModel.DataAnnotations;

//namespace Surveillance.Domain.Entities
//{


//    public class CaseRecord
//    {
//        public int Id { get; set; }

//        [Required(ErrorMessage = "Patient name is required")]
//        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
//        public string PatientName { get; set; } = "";

//        [Required(ErrorMessage = "Phone number is mandatory")]
//        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter valid 10-digit mobile number")]
//        public string Phone { get; set; } = "";

//       // [Required(ErrorMessage = "Disease name is required")]
//        [StringLength(100, ErrorMessage = "Disease name too long")]
//        public string DiseaseName { get; set; } = string.Empty;


//        /// <summary>
//        ///Docs always ask when your symptoms started...It's a handwritten note doc may or may not write...
//        /// Db saved symptoms are there below as CaseRecordSymptoms
//        /// </summary>

//        [Required(ErrorMessage = "Symptoms are required")]
//        [StringLength(500, ErrorMessage = "Symptoms too long (max 500 characters)")]
//        public string Symptoms { get; set; } = "";        

//        [Required(ErrorMessage = "When exactly the symptoms started must be recorded")]
//        public DateTime OnsetDate { get; set; }

//        [Required(ErrorMessage = "Address is required")]
//        [StringLength(300, ErrorMessage = "Address too long (max 300 characters)")]
//        public string AddressOfPatient { get; set; } = "";

//        public bool IsCommunicable { get; set; }

//        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

//        [Required(ErrorMessage = "Patient visit date is required")]
//        public DateTime DateReported { get; set; }

//        public DateTime? LabConfirmedDate { get; set; }

//        public CaseStatus Status { get; set; } = CaseStatus.Suspected;

//        //[Required]
//        public string UserId { get; set; } = "";

//        public ApplicationUser User { get; set; } = default!;

//        [Required(ErrorMessage = "Medical Facility is required")]
//        public int FacilityId { get; set; }

//        public Facility Facility { get; set; } = default!;

//        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

//        public ICollection<CaseRecordSymptom> CaseRecordSymptoms { get; set; }
//= new List<CaseRecordSymptom>();

//        public ICollection<Sample> Samples { get; set; }
//        = new List<Sample>();

//        public ICollection<CaseRecordLabTest> LabTests { get; set; }
//        = new List<CaseRecordLabTest>();

//    }
//}
















//using System.ComponentModel.DataAnnotations;

//namespace UPHC.SurveillanceDashboard.Models
//{


//    public enum CaseStatus
//    {
//        Suspected,
//        Confirmed,
//        Negative
//    }

//    public class CaseRecord
//    {
//        public int Id { get; set; }

//        [Required(ErrorMessage = "Patient name is required")]
//        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
//        public string PatientName { get; set; } = "";

//        [Required(ErrorMessage = "Phone number is mandatory")]
//        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter valid 10-digit mobile number")]
//        public string Phone { get; set; } = "";



//        [Required(ErrorMessage = "Disease name is required")]
//        [StringLength(100, ErrorMessage = "Disease name too long")]
//        public string DiseaseName { get; set; } = "";


//        [StringLength(500, ErrorMessage = "Symptoms too long (max 500 characters)")]
//        [Required(ErrorMessage = "Symptom is  required")]
//        public string Symptoms { get; set; } = "";

//        [Required(ErrorMessage ="When exactly the symptoms started must be recorded")]
//        public DateTime OnsetDate { get; set; } // Docs always ask when your symptoms started

//        [Required(ErrorMessage = "Address is required")]
//        [StringLength(300, ErrorMessage = "Address too long (max 300 characters)")]
//        public string AddressOfPatient { get; set; } = "";
//        public bool IsCommunicable { get; set; }

//        public DateTime CreatedDate { get; set; } = DateTime.Now; //system entry date or when record is saved to database and can be equal to DateReported

//        [Required(ErrorMessage = "Patient visit date is required")]
//        public DateTime? DateReported { get; set; }  // Date when patient visited UPHC / was seen by doctor Or when the patient arrives at UPHC and  can be same as Created Date OR Consultation Date

//        public DateTime? LabConfirmedDate { get; set; } //Lab confirmation date when "Lab result is out"
//        public CaseStatus Status { get; set; } = CaseStatus.Suspected; // Suspected / Confirmed / Negative


//        [Required]
//        public string UserId { get; set; } = "";

//        public ApplicationUser? User { get; set; }



//        [Required(ErrorMessage = "Medical Facility is required")]

//        public int FacilityId { get; set; }
//        public Facility? Facility { get; set; }

//        // 🔗 One Case → Many Notifications
//        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();


//    }
//}
