namespace Surveillance.Domain.Entities
{
    public class SampleNotificationDetails
    {
        public Guid SampleId { get; set; }

        public int CaseRecordId { get; set; }

        public int FacilityId { get; set; }
    }
}