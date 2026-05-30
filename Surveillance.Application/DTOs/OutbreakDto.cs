namespace UPHC.SurveillanceDashboard.Models
{
    public class OutbreakDto
    {
        public string DiseaseName { get; set; } =string.Empty;
        public string FacilityName { get; set; } = string.Empty;
        public int Count { get; set; }
        public DateTime FirstCaseDate { get; set; }
    }
    // This DTO can be used to represent outbreak information in the dashboard, such as:
    // - Disease Name
    // - Facility Name
    // - Number of Cases
    // - Date of First Case

    //Used for Confirmed Outbreaks section in the dashboard
    //Used for SUSPECTED CLUSTERS section in the dashboard
}
