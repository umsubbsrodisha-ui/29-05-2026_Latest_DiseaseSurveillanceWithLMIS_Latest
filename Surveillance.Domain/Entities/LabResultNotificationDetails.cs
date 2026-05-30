namespace Surveillance.Domain.Entities
{
    public class LabResultNotificationDetails
    {
        public Guid CaseRecordLabTestId { get; set; }

        public int CaseRecordId { get; set; }

        public int FacilityId { get; set; }
    }
}