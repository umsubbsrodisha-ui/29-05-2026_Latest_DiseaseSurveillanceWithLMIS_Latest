using Surveillance.Application.DTOs;
using Surveillance.Domain.Entities;
using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using UPHC.SurveillanceDashboard.Models;

namespace Surveillance.Application.Interfaces.Repositories
{

    public interface IRepository
    {
        // ============ CASE RECORDS ============
        Task<CaseRecordDetails?> GetCaseDetailsAsync(int caseId);
        Task<List<Symptom>> GetAllSymptomsAsync();

        Task<List<SampleType>> GetAllSampleTypesAsync();

        Task<CaseRecord?> GetCaseById_ForUpdateAsync(int id);
        Task<CaseRecordDetails?> GetCaseByIdAsync(int id);

        Task<int> GetCommunicableCaseCountAsync(DateTime fromDate);

        Task<List<OutbreakDto>>
            GetConfirmedOutbreaksAsync(DateTime fromDate);

        Task<List<OutbreakDto>>
            GetSuspectedClustersAsync(DateTime fromDate);

        Task<List<FacilityCaseInfo>> GetCasesByFacilityAsync(
            int facilityId,
            int page = 1,
            int pageSize = 10);

        Task<int> GetCasesCountByFacilityAsync(int facilityId);

       // Task<CaseRecord> AddCaseAsync(CaseRecord caseRecord, List<int> symptomIds, List<int> sampleTypeIds);
        Task<CaseRecord> AddCaseAsync(
    CaseRecord caseRecord,
    List<int> symptomIds,
    List<int> sampleTypeIds,
    List<int> labTestIds);
        Task UpdateCaseAsync(CaseRecord caseRecord);

        Task<List<CommunicableRecordListItem>> GetCommunicableRecordsAsync(int pageNumber,int pageSize);



        // ============ FACILITIES ============

        Task<List<FacilityDiseaseData>> GetAllFacilitiesAsync();

        Task<FacilityInfo?> GetFacilityByIdAsync(int id);

        Task<List<FacilityStatDto>> GetFacilityStatsAsync(DateTime fromDate);

        // ============ DASHBOARD DATA ============
        Task<List<TrendPointDto>> GetTrendDataAsync(DateTime fromDate);

        Task<List<DiseaseDistributionDto>> GetDiseaseDistributionAsync(DateTime fromDate);

        Task<List<FacilityChartDto>> GetFacilityChartDataAsync(DateTime fromDate);


        // ============ NOTIFICATIONS ============

        Task<List<Notification>> GetNotificationsByFacilityAsync(int facilityId);

        Task<List<NotificationDetails>> GetAllNotificationsAsync();

        Task<Notification?> GetNotificationByIdAsync(int id);

        Task AddNotificationAsync(Notification notification);

        Task UpdateNotificationAsync(Notification notification);

        //LabTest
        Task<List<LabTest>> GetAllLabTestsAsync();

        Task<List<SampleQueueDetails>> GetSampleQueueByFacilityAsync(int facilityId);

        Task<bool> UpdateSampleStatusAsync(
    Guid sampleId,
    SampleStatus newStatus,
    string? barcode,
    string? collectedBy,
    string? collectionNotes,
    string? dispatchReferenceNo);

        Task<Guid?> EnterLabResultAsync(
     Guid caseRecordLabTestId,
     string resultValue,
     LabResultStatus resultStatus,
     string? enteredByUserId,
     bool isVerified,
     string? verifiedByUserId,
     string? remarks,
     string? reportPath);
        Task<List<LabWorkbenchDetails>> GetLabWorkbenchByFacilityAsync(int facilityId);


        Task<SampleNotificationDetails?> GetSampleNotificationDetailsAsync(Guid sampleId);

        Task<LabResultNotificationDetails?> GetLabResultNotificationDetailsAsync(
    Guid caseRecordLabTestId);

        Task<LabReportDetails?> GetLabReportByIdAsync(Guid labResultId);

        Task<CaseEditDetails?> GetCaseForEditAsync(int id);

        Task<bool> UpdateCaseDetailsAsync(
    int id,
    string patientName,
    string phone,
    string diseaseName,
    string addressOfPatient,
    DateTime onsetDate,
    DateTime dateReported,
    string? clinicalNotes,
    List<int> symptomIds,
    List<int> sampleTypeIds,
    List<int> labTestIds);




        Task<CaseLabReportDto?> GetCaseLabReportByCaseIdAsync(
    int caseRecordId);

        Task<bool> AreAllLabTestsVerifiedForCaseAsync(int caseRecordId);

        Task<bool> HasAnyPositiveLabResultForCaseAsync(int caseRecordId);


        Task<List<NotificationDto>> GetNotificationsForUserAsync(string userId);

        Task MarkNotificationsReadForUserAsync(
            string userId,
            List<int> notificationIds);


        // ============ SAVE ============

        Task SaveChangesAsync();
    }

  
}











