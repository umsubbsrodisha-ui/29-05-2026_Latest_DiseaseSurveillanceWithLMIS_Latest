using Microsoft.EntityFrameworkCore;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;
using Surveillance.Domain.Entities;
using Surveillance.Domain.Enums;
using Surveillance.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using UPHC.SurveillanceDashboard.Models;
using static System.Net.WebRequestMethods;
//using Surveillance.Domain.Entities;


namespace Surveillance.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public Repository(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<SampleQueueDetails>> GetSampleQueueByFacilityAsync(int facilityId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.Samples
                .AsNoTracking()
                .Where(s => facilityId == 0 || s.CaseRecord.FacilityId == facilityId)
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => new SampleQueueDetails
                {
                    SampleId = s.Id,

                    CaseRecordId = s.CaseRecordId,

                    PatientName = s.CaseRecord.PatientName,

                    Phone = s.CaseRecord.Phone,

                    DiseaseName = s.CaseRecord.DiseaseName,

                    FacilityId = s.CaseRecord.FacilityId,

                    FacilityName = s.CaseRecord.Facility.FacilityName,

                    SampleTypeId = s.SampleTypeId,

                    SampleTypeName = s.SampleType.Name,

                    Status = s.Status,

                    Barcode = s.Barcode,

                    CollectedBy = s.CollectedBy,

                    CollectionNotes = s.CollectionNotes,

                    CollectedAt = s.CollectedAt,

                    DispatchedAt = s.DispatchedAt,

                    ReceivedAtLabAt = s.ReceivedAtLabAt,

                    DispatchReferenceNo = s.DispatchReferenceNo,

                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();
        }

        // ============ CASE RECORDS ============

        public async Task<CaseRecord?> GetCaseById_ForUpdateAsync(int id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords.AsNoTracking()
                .Include(c => c.Facility)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CaseRecordDetails?> GetCaseDetailsAsync(
    int caseId)
        {
            using var db =
                await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords
                .AsNoTracking()
                .Where(c => c.Id == caseId)
                .Select(c => new CaseRecordDetails
                {
                    // =================================================
                    // BASIC
                    // =================================================

                    Id = c.Id,

                    PatientName = c.PatientName,

                    Phone = c.Phone,

                    DiseaseName = c.DiseaseName,

                    AddressOfPatient = c.AddressOfPatient,

                    Notes = c.ClinicalNotes,

                    // =================================================
                    // DATES
                    // =================================================

                    OnsetDate = c.OnsetDate,

                    DateReported = c.DateReported,

                    CreatedDate = c.CreatedDate,

                    LabConfirmedDate = c.LabConfirmedDate,

                    // =================================================
                    // STATUS
                    // =================================================

                    IsCommunicable = c.IsCommunicable,

                    Status = c.Status,

                    // =================================================
                    // FACILITY
                    // =================================================

                    FacilityId = c.FacilityId,

                    FacilityName = c.Facility.FacilityName,

                    // =================================================
                    // USER
                    // =================================================

                    UserId = c.UserId,

                    CreatedByName = c.User.UserName,

                    // =================================================
                    // SYMPTOMS
                    // =================================================

                    Symptoms = c.CaseRecordSymptoms
                        .Select(x => x.Symptom.Name)
                        .ToList(),

                    // =================================================
                    // SAMPLES
                    // =================================================

                    Samples = c.Samples
                        .Select(s => new CaseSampleDetails
                        {
                            SampleId = s.Id,

                            SampleType = s.SampleType.Name,

                            SampleStatus = s.Status,

                            Barcode = s.Barcode,

                            DispatchReferenceNo =
                                s.DispatchReferenceNo,

                            CollectedAt = s.CollectedAt,

                            CollectedBy = s.CollectedBy,

                            CollectionNotes =
                                s.CollectionNotes,

                            DispatchedAt = s.DispatchedAt,

                            ReceivedAtLabAt =
                                s.ReceivedAtLabAt,

                            ProcessingFacilityId =
                                s.ProcessingFacilityId,

                            ProcessingFacilityName =
                                s.ProcessingFacility != null
                                    ? s.ProcessingFacility.FacilityName
                                    : null
                        })
                        .ToList(),

                    // =================================================
                    // LAB TESTS
                    // =================================================

                    LabTests = c.LabTests
                        .Select(t => new CaseLabTestDetails
                        {
                            CaseRecordLabTestId = t.Id,

                            LabTestName = t.LabTest.Name,

                            LabResultStatus =
                                  t.LabResults
                .OrderByDescending(r => r.EnteredAt)
                .Select(r => r.ResultStatus)
                .FirstOrDefault(),

                            TestedAt = t.TestedAt,

                            ResultValue =
            t.LabResults
                .OrderByDescending(r => r.EnteredAt)
                .Select(r => r.ResultValue)
                .FirstOrDefault(),

                            Remarks =
            t.LabResults
                .OrderByDescending(r => r.EnteredAt)
                .Select(r => r.Remarks)
                .FirstOrDefault(),

                            ReportPath = t.ReportPath,

                            SampleId = t.SampleId,

                            SampleBarcode =
            t.Sample.Barcode
                        })
    .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CaseRecordDetails?> GetCaseByIdAsync(int id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CaseRecordDetails
                {
                    Id = c.Id,
                    PatientName = c.PatientName,
                    Phone = c.Phone,
                    DiseaseName = c.DiseaseName,
                    AddressOfPatient = c.AddressOfPatient,

                    CreatedDate = c.CreatedDate,
                    OnsetDate = c.OnsetDate,
                    DateReported = c.DateReported,
                    LabConfirmedDate = c.LabConfirmedDate,

                    IsCommunicable = c.IsCommunicable,
                    Status = c.Status,

                    FacilityId = c.FacilityId,
                    FacilityName = c.Facility.FacilityName,

                    UserId = c.UserId,
                    CreatedByName = c.User.UserName,

                    Symptoms = c.CaseRecordSymptoms
                        .Select(x => x.Symptom.Name)
                        .ToList(),

                    Samples = c.Samples
                        .Select(s => new CaseSampleDetails
                        {
                            SampleId = s.Id,
                            SampleType = s.SampleType.Name,
                            SampleStatus = s.Status,
                            Barcode = s.Barcode,
                            DispatchReferenceNo = s.DispatchReferenceNo,
                            CollectedAt = s.CollectedAt,
                            CollectedBy = s.CollectedBy,
                            CollectionNotes = s.CollectionNotes,
                            DispatchedAt = s.DispatchedAt,
                            ReceivedAtLabAt = s.ReceivedAtLabAt,
                            ProcessingFacilityId = s.ProcessingFacilityId,
                            ProcessingFacilityName = s.ProcessingFacility != null
                                ? s.ProcessingFacility.FacilityName
                                : null
                        })
                        .ToList(),

                    LabTests = c.LabTests
                        .Select(t => new CaseLabTestDetails
                        {
                            CaseRecordLabTestId = t.Id,
                            LabTestName = t.LabTest.Name,

                            LabResultStatus = t.LabResults
                                .OrderByDescending(r => r.EnteredAt)
                                .Select(r => r.ResultStatus)
                                .FirstOrDefault(),

                            TestedAt = t.TestedAt,

                            ResultValue = t.LabResults
                                .OrderByDescending(r => r.EnteredAt)
                                .Select(r => r.ResultValue)
                                .FirstOrDefault(),

                            Remarks = t.LabResults
                                .OrderByDescending(r => r.EnteredAt)
                                .Select(r => r.Remarks)
                                .FirstOrDefault(),

                            LabResultId = t.LabResults
                                  .OrderByDescending(r => r.EnteredAt)
            .Select(r => (Guid?)r.Id)
            .FirstOrDefault(),

                            IsVerified = t.LabResults
            .OrderByDescending(r => r.EnteredAt)
            .Select(r => r.IsVerified)
            .FirstOrDefault(),

                            ReportPath = t.ReportPath,
                            SampleId = t.SampleId,
                            SampleBarcode = t.Sample.Barcode
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }



        public async Task<int> GetCommunicableCaseCountAsync(DateTime fromDate)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords
                .AsNoTracking()
                .CountAsync(c =>
                    c.IsCommunicable &&
                    c.OnsetDate >= fromDate.Date);
        }

        public async Task<List<OutbreakDto>> GetConfirmedOutbreaksAsync(DateTime fromDate)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords
                .AsNoTracking()
                .Where(c =>
                    c.IsCommunicable &&
                    c.Status == CaseStatus.Confirmed &&
                    c.OnsetDate >= fromDate.Date)
                .GroupBy(c => new
                {
                    c.DiseaseName,
                    FacilityName = c.Facility.FacilityName
                })
                .Where(g => g.Count() >= 3)
                .Select(g => new OutbreakDto
                {
                    DiseaseName = g.Key.DiseaseName,
                    FacilityName = g.Key.FacilityName,
                    Count = g.Count(),
                    FirstCaseDate = g.Min(x => x.OnsetDate)
                })
                .ToListAsync();
        }

        public async Task<List<OutbreakDto>> GetSuspectedClustersAsync(DateTime fromDate)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords
                .AsNoTracking()
                .Where(c =>
                    c.IsCommunicable &&
                    c.Status == CaseStatus.Suspected &&
                    c.OnsetDate >= fromDate.Date)
                .GroupBy(c => new
                {
                    c.DiseaseName,
                    FacilityName = c.Facility.FacilityName
                })
                .Where(g => g.Count() >= 5)
                .Select(g => new OutbreakDto
                {
                    DiseaseName = g.Key.DiseaseName,
                    FacilityName = g.Key.FacilityName,
                    Count = g.Count(),
                    FirstCaseDate = g.Min(x => x.OnsetDate)
                })
                .ToListAsync();
        }

        //Discarded
        //public async Task<List<CaseRecord>> GetCommunicableCasesFromDateAsync(DateTime fromDate)
        //{
        //    using var db = await _dbFactory.CreateDbContextAsync();

        //    return await db.CaseRecords
        //        .Include(c => c.Facility)
        //        .Where(c => c.IsCommunicable && c.OnsetDate >= fromDate.Date)
        //        .ToListAsync();
        //}

        public async Task<List<FacilityCaseInfo>> GetCasesByFacilityAsync(int facilityId, int page = 1, int pageSize = 10)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            //return await db.CaseRecords.AsNoTracking()
            //    .Where(c => c.FacilityId == facilityId)
            //    .OrderByDescending(c => c.CreatedDate)
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();

            return await db.CaseRecords
       .AsNoTracking()
       .Where(c => facilityId == 0 || c.FacilityId == facilityId)
       .OrderByDescending(c => c.CreatedDate)
       .Skip((page - 1) * pageSize)
       .Take(pageSize)
       .Select(c => new FacilityCaseInfo
       {
           Id = c.Id,
           CreatedDate = c.CreatedDate,
           DiseaseName = c.DiseaseName,
           PatientName = c.PatientName
       })
       .ToListAsync();
        }


        //optimized
        public async Task<int> GetCasesCountByFacilityAsync(int facilityId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords.AsNoTracking()
                .Where(c => facilityId == 0 || c.FacilityId == facilityId)
                .CountAsync();
        }

        public async Task<CaseRecord> AddCaseAsync(CaseRecord caseRecord, List<int> symptomIds, List<int> sampleTypeIds, List<int> labTestIds)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            using var transaction = await db.Database.BeginTransactionAsync();

            try
            {
                // =====================================================
                // SAVE MAIN CASE RECORD
                // =====================================================

                db.CaseRecords.Add(caseRecord);

                await db.SaveChangesAsync();

                // =====================================================
                // SAVE CASE SYMPTOMS
                // MANY TO MANY : CaseRecordSymptoms
                // =====================================================

                if (symptomIds != null && symptomIds.Any())
                {
                    var caseSymptoms = symptomIds
                        .Distinct()
                        .Select(symptomId => new CaseRecordSymptom
                        {
                            CaseRecordId = caseRecord.Id,
                            SymptomId = symptomId
                        })
                        .ToList();

                    await db.CaseRecordSymptoms.AddRangeAsync(caseSymptoms);
                }

                // =====================================================
                // CREATE SAMPLE RECORDS
                // =====================================================

                var createdSamples = new List<Sample>();

                if (sampleTypeIds != null && sampleTypeIds.Any())
                {
                    createdSamples = sampleTypeIds
                        .Distinct()
                        .Select(sampleTypeId => new Sample
                        {
                            CaseRecordId = caseRecord.Id,
                            SampleTypeId = sampleTypeId,
                            Status = SampleStatus.PendingCollection,
                            CreatedAt = DateTime.UtcNow,
                            Barcode = $"TMP-{Guid.NewGuid():N}",
                            CollectionNotes = "Awaiting sample collection"
                        })
                        .ToList();

                    await db.Samples.AddRangeAsync(createdSamples);

                    // Required so Sample.Id values are available for CaseRecordLabTest
                    await db.SaveChangesAsync();
                }

                // =====================================================
                // CREATE REQUESTED LAB TESTS
                // CaseRecordLabTests
                // NOTE: LabResults are NOT created here.
                // =====================================================

                if (labTestIds != null && labTestIds.Any() && createdSamples.Any())
                {
                    var caseLabTests = new List<CaseRecordLabTest>();

                    foreach (var sample in createdSamples)
                    {
                        foreach (var labTestId in labTestIds.Distinct())
                        {
                            caseLabTests.Add(new CaseRecordLabTest
                            {
                                Id = Guid.NewGuid(),
                                CaseRecordId = caseRecord.Id,
                                SampleId = sample.Id,
                                LabTestId = labTestId,
                                TestedAt = null,
                                ReportPath = null
                            });
                        }
                    }

                    await db.CaseRecordLabTests.AddRangeAsync(caseLabTests);
                }

                // =====================================================
                // FINAL SAVE
                // =====================================================

                await db.SaveChangesAsync();

                await transaction.CommitAsync();

                return caseRecord;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }




        public async Task UpdateCaseAsync(CaseRecord caseRecord)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            db.CaseRecords.Update(caseRecord);
            await db.SaveChangesAsync();
        }


        public async Task<bool> UpdateCaseDetailsAsync(
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
    List<int> labTestIds)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            using var transaction = await db.Database.BeginTransactionAsync();

            try
            {
                var caseRecord = await db.CaseRecords
                    .Include(c => c.CaseRecordSymptoms)
                    .Include(c => c.Samples)
                    .Include(c => c.LabTests)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (caseRecord == null)
                    return false;

                caseRecord.PatientName = patientName;
                caseRecord.Phone = phone;
                caseRecord.DiseaseName = diseaseName;
                caseRecord.AddressOfPatient = addressOfPatient;
                caseRecord.OnsetDate = onsetDate;
                caseRecord.DateReported = dateReported;
                caseRecord.ClinicalNotes = clinicalNotes;

                // Replace symptoms
                db.CaseRecordSymptoms.RemoveRange(caseRecord.CaseRecordSymptoms);

                var newSymptoms = symptomIds
                    .Distinct()
                    .Select(symptomId => new CaseRecordSymptom
                    {
                        CaseRecordId = id,
                        SymptomId = symptomId
                    })
                    .ToList();

                await db.CaseRecordSymptoms.AddRangeAsync(newSymptoms);

                // Add only missing samples
                var existingSampleTypeIds = caseRecord.Samples
                    .Select(s => s.SampleTypeId)
                    .ToHashSet();

                var newSamples = sampleTypeIds
                    .Distinct()
                    .Where(sampleTypeId => !existingSampleTypeIds.Contains(sampleTypeId))
                    .Select(sampleTypeId => new Sample
                    {
                        CaseRecordId = id,
                        SampleTypeId = sampleTypeId,
                        Status = SampleStatus.PendingCollection,
                        CreatedAt = DateTime.UtcNow,
                        Barcode = string.Empty,
                        CollectionNotes = "Awaiting sample collection"
                    })
                    .ToList();

                if (newSamples.Any())
                {
                    await db.Samples.AddRangeAsync(newSamples);
                    await db.SaveChangesAsync();
                }

                // Reload samples after adding new ones
                var allSamples = await db.Samples
                    .Where(s => s.CaseRecordId == id)
                    .ToListAsync();

                // Add only missing lab test/sample combinations
                var existingLabPairs = await db.CaseRecordLabTests
                    .Where(x => x.CaseRecordId == id)
                    .Select(x => new
                    {
                        x.SampleId,
                        x.LabTestId
                    })
                    .ToListAsync();

                var newCaseLabTests = new List<CaseRecordLabTest>();

                foreach (var sample in allSamples)
                {
                    foreach (var labTestId in labTestIds.Distinct())
                    {
                        bool alreadyExists = existingLabPairs.Any(x =>
                            x.SampleId == sample.Id &&
                            x.LabTestId == labTestId);

                        if (!alreadyExists)
                        {
                            newCaseLabTests.Add(new CaseRecordLabTest
                            {
                                Id = Guid.NewGuid(),
                                CaseRecordId = id,
                                SampleId = sample.Id,
                                LabTestId = labTestId,
                                TestedAt = null,
                                ReportPath = null
                            });
                        }
                    }
                }

                if (newCaseLabTests.Any())
                {
                    await db.CaseRecordLabTests.AddRangeAsync(newCaseLabTests);
                }

                await db.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }



        // ============ FACILITIES ============

        public async Task<List<FacilityDiseaseData>> GetAllFacilitiesAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            //return await db.Facilities
            //    .Include(f => f.CaseRecords)
            //    .ToListAsync();




            var fromDate = DateTime.UtcNow.AddDays(-14);

            return await db.CaseRecords
                .AsNoTracking()
                .Where(c =>
                    c.IsCommunicable &&
                    c.OnsetDate >= fromDate)
                .GroupBy(c => new
                {
                    c.FacilityId,
                    c.Facility.FacilityName,
                    c.DiseaseName
                })
                .Select(g => new FacilityDiseaseData
                {
                    FacilityId = g.Key.FacilityId,
                    FacilityName = g.Key.FacilityName,
                    DiseaseName = g.Key.DiseaseName,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();













        }


        // optimized

        public async Task<FacilityInfo?> GetFacilityByIdAsync(int id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            if (id == 0)
            {
                return new FacilityInfo
                {
                    FacilityName = "All Facilities",
                    FacilityAddress = "System Wide View"
                };
            }


            return await db.Facilities
       .AsNoTracking()
       .Where(f => f.Id == id)
       .Select(f => new FacilityInfo
       {
           FacilityName = f.FacilityName,
           FacilityAddress = f.FacilityAddress
       })
       .FirstOrDefaultAsync();

            //return await db.Facilities
            //    .Include(f => f.CaseRecords)
            //    .FirstOrDefaultAsync(f => f.Id == id);
        }



        public async Task<List<FacilityStatDto>> GetFacilityStatsAsync(DateTime fromDate)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.Facilities
                .AsNoTracking()
                .Select(f => new FacilityStatDto
                {
                    Id = f.Id,
                    Name = f.FacilityName,

                    TotalCases = f.CaseRecords.Count(c =>
                        c.IsCommunicable &&
                        c.OnsetDate >= fromDate)
                })
                .ToListAsync();
        }


        public async Task<List<TrendPointDto>> GetTrendDataAsync(DateTime fromDate)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            var groupedData = await db.CaseRecords
                .AsNoTracking()
                .Where(c =>
                    c.IsCommunicable &&
                    c.OnsetDate >= fromDate.Date)
                .GroupBy(c => c.OnsetDate.Date)
                .Select(g => new
                {
                    Date = g.Key,

                    ConfirmedCount = g.Count(c =>
                        c.Status == CaseStatus.Confirmed),

                    SuspectedCount = g.Count(c =>
                        c.Status == CaseStatus.Suspected)
                })
                .ToListAsync();

            var trend = new List<TrendPointDto>();

            for (int i = 13; i >= 0; i--)
            {
                var date = DateTime.UtcNow.Date.AddDays(-i);

                var existing = groupedData
                    .FirstOrDefault(x => x.Date == date);

                trend.Add(new TrendPointDto
                {
                    Label = date.ToString("dd MMM"),

                    ConfirmedCount =
                        existing?.ConfirmedCount ?? 0,

                    SuspectedCount =
                        existing?.SuspectedCount ?? 0
                });
            }

            return trend;
        }

        public async Task<List<DiseaseDistributionDto>> GetDiseaseDistributionAsync(DateTime fromDate)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords
                .AsNoTracking()
                .Where(c =>
                    c.IsCommunicable &&
                    c.OnsetDate >= fromDate.Date)
                .GroupBy(c => c.DiseaseName)
                .Select(g => new DiseaseDistributionDto
                {
                    DiseaseName = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(6)
                .ToListAsync();
        }


        public async Task<List<FacilityChartDto>> GetFacilityChartDataAsync(DateTime fromDate)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            /////////////Optimized execution plan - CaseRecords//////////////
            ///

            // CaseRecords
            //     ↓
            // Filter first
            //     ↓-----------------Execution plan will show this as a clustered index scan with a filter on IsCommunicable and OnsetDate
            //GROUP BY Facility
            //     ↓
            //   COUNT
            //return await db.CaseRecords.AsNoTracking().Where(c => c.IsCommunicable && c.OnsetDate >= fromDate).
            //    GroupBy(c => new { c.FacilityId, c.Facility.FacilityName }).
            //    Select(g => new FacilityChartDto { FacilityName = g.Key.FacilityName, Count = g.Count() }).
            //    OrderByDescending(x => x.Count).Take(8).ToListAsync();

            //No so optimized execution plan - Facilities///


            //////Not optimized..Old execution plan - Facilities
            ////                           ↓
            ///                           Count CaseRecords per facility---> old execution plan
            ///                            ↓
            /////                         Correlated subqueries


            return await db.Facilities
                .AsNoTracking()
                .Select(f => new FacilityChartDto
                {
                    FacilityName = f.FacilityName,

                    Count = f.CaseRecords.Count(c =>
                        c.IsCommunicable &&
                        c.OnsetDate >= fromDate)
                })
                .OrderByDescending(x => x.Count)
                .Take(8)
                .ToListAsync();




        }

        // ============ NOTIFICATIONS ============

        public async Task<List<Notification>> GetNotificationsByFacilityAsync(int facilityId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.Notifications
                .Include(n => n.Facility)
                .Include(n => n.CaseRecord)
                .Where(n => n.FacilityId == facilityId)
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();
        }

        public async Task<List<NotificationDetails>> GetAllNotificationsAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.Notifications.AsNoTracking().OrderByDescending(n => n.Timestamp).Take(50).
                Select(n => new NotificationDetails
                {
                    Id = n.Id,
                    FacilityId = n.FacilityId,
                    CaseRecordId = n.CaseRecordId,
                    Type = n.Type.ToString(),
                    IsChecked = n.IsChecked,
                    Timestamp = n.Timestamp,
                    FacilityName = n.Facility.FacilityName,
                    DiseaseName = n.CaseRecord.DiseaseName,
                    Status = n.CaseRecord.Status,
                    LabResultId = n.LabResultId
                }).ToListAsync();

            //return await db.Notifications.AsNoTracking()
            //    .Include(n => n.Facility)
            //    .Include(n => n.CaseRecord)
            //    .OrderByDescending(n => n.Timestamp)
            //    .Take(50)
            //    .ToListAsync();
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.Notifications
                .Include(n => n.Facility)
                .Include(n => n.CaseRecord)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            db.Notifications.Add(notification);
            await db.SaveChangesAsync();
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            db.Notifications.Update(notification);
            await db.SaveChangesAsync();
        }

        // ============ SAVE ============

        public async Task SaveChangesAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            await db.SaveChangesAsync();
        }

        public async Task<List<CommunicableRecordListItem>> GetCommunicableRecordsAsync(int pageNumber, int pageSize)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords

                .AsNoTracking()

                .Where(c => c.IsCommunicable)

                .OrderByDescending(c => c.OnsetDate)

                .Skip((pageNumber - 1) * pageSize)

                .Take(pageSize)

                .Select(c => new CommunicableRecordListItem
                {
                    Id = c.Id,

                    PatientName = c.PatientName,

                    FacilityName = c.Facility.FacilityName,

                    OnsetDate = c.OnsetDate
                })

                .ToListAsync();
        }

        public async Task<List<Symptom>> GetAllSymptomsAsync()
        {
            using var db =
       await _dbFactory.CreateDbContextAsync();

            return await db.Symptoms
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<SampleType>> GetAllSampleTypesAsync()
        {
            using var db =
       await _dbFactory.CreateDbContextAsync();

            return await db.SampleTypes
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }



        public async Task<List<LabTest>> GetAllLabTestsAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.LabTests
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<bool> UpdateSampleStatusAsync(
            Guid sampleId,
            SampleStatus newStatus,
            string? barcode,
            string? collectedBy,
            string? collectionNotes,
            string? dispatchReferenceNo)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            var sample = await db.Samples
                .FirstOrDefaultAsync(s => s.Id == sampleId);

            if (sample == null)
                return false;

            sample.Status = newStatus;

            if (!string.IsNullOrWhiteSpace(barcode))
                sample.Barcode = barcode;

            if (!string.IsNullOrWhiteSpace(collectedBy))
                sample.CollectedBy = collectedBy;

            if (!string.IsNullOrWhiteSpace(collectionNotes))
                sample.CollectionNotes = collectionNotes;

            if (!string.IsNullOrWhiteSpace(dispatchReferenceNo))
                sample.DispatchReferenceNo = dispatchReferenceNo;

            if (newStatus == SampleStatus.Collected)
            {
                sample.CollectedAt = DateTime.UtcNow;
            }

            if (newStatus == SampleStatus.Dispatched)
            {
                sample.DispatchedAt = DateTime.UtcNow;
            }

            if (newStatus == SampleStatus.Received)
            {
                sample.ReceivedAtLabAt = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();

            return true;
        }

        public async Task<List<LabWorkbenchDetails>> GetLabWorkbenchByFacilityAsync(int facilityId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecordLabTests
                .AsNoTracking()
                .Where(t => facilityId == 0 || t.CaseRecord.FacilityId == facilityId)
                .Where(t => t.Sample.Status == SampleStatus.Received
                         || t.Sample.Status == SampleStatus.Processing
                         || t.Sample.Status == SampleStatus.Tested)
                .OrderByDescending(t => t.Sample.ReceivedAtLabAt)
                .Select(t => new LabWorkbenchDetails
                {
                    CaseRecordLabTestId = t.Id,

                    CaseRecordId = t.CaseRecordId,

                    PatientName = t.CaseRecord.PatientName,

                    Phone = t.CaseRecord.Phone,

                    DiseaseName = t.CaseRecord.DiseaseName,

                    FacilityId = t.CaseRecord.FacilityId,

                    FacilityName = t.CaseRecord.Facility.FacilityName,

                    SampleId = t.SampleId,

                    SampleBarcode = t.Sample.Barcode,

                    SampleTypeName = t.Sample.SampleType.Name,

                    SampleStatus = t.Sample.Status,

                    LabTestId = t.LabTestId,

                    LabTestName = t.LabTest.Name,

                    TestedAt = t.TestedAt,

                    ReportPath = t.ReportPath,

                    LatestResultValue = t.LabResults
                        .OrderByDescending(r => r.EnteredAt)
                        .Select(r => r.ResultValue)
                        .FirstOrDefault(),

                    LatestResultStatus = t.LabResults
                        .OrderByDescending(r => r.EnteredAt)
                        .Select(r => (LabResultStatus?)r.ResultStatus)
                        .FirstOrDefault(),

                    LatestResultEnteredAt = t.LabResults
                        .OrderByDescending(r => r.EnteredAt)
                        .Select(r => (DateTime?)r.EnteredAt)
                        .FirstOrDefault(),

                    LatestResultIsVerified = t.LabResults
                        .OrderByDescending(r => r.EnteredAt)
                        .Select(r => (bool?)r.IsVerified)
                        .FirstOrDefault(),

                    LatestResultRemarks = t.LabResults
                        .OrderByDescending(r => r.EnteredAt)
                        .Select(r => r.Remarks)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }


        public async Task<Guid?> EnterLabResultAsync(
       Guid caseRecordLabTestId,
       string resultValue,
       LabResultStatus resultStatus,
       string? enteredByUserId,
       bool isVerified,
       string? verifiedByUserId,
       string? remarks,
       string? reportPath)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            var caseLabTest = await db.CaseRecordLabTests
                .Include(x => x.Sample)
                .Include(x => x.CaseRecord)
                .FirstOrDefaultAsync(x => x.Id == caseRecordLabTestId);

            if (caseLabTest == null)
                return null;

            var labResult = new LabResult
            {
                Id = Guid.NewGuid(),
                CaseRecordLabTestId = caseLabTest.Id,
                ResultValue = resultValue,
                ResultStatus = resultStatus,
                EnteredAt = DateTime.UtcNow,
                EnteredByUserId = enteredByUserId,
                IsVerified = isVerified,
                VerifiedByUserId = verifiedByUserId,
                Remarks = remarks,
                ReportLink = string.Empty
            };

            labResult.ReportLink = $"/lab-report/{labResult.Id}";

            db.LabResults.Add(labResult);

            caseLabTest.TestedAt = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(reportPath))
                caseLabTest.ReportPath = reportPath;

            caseLabTest.Sample.Status = SampleStatus.Tested;

            if (isVerified)
            {
                if (resultStatus == LabResultStatus.Positive)
                {
                    caseLabTest.CaseRecord.Status = CaseStatus.Confirmed;
                    caseLabTest.CaseRecord.LabConfirmedDate = DateTime.UtcNow;
                }
                else if (resultStatus == LabResultStatus.Negative)
                {
                    caseLabTest.CaseRecord.Status = CaseStatus.Negative;
                    caseLabTest.CaseRecord.LabConfirmedDate = DateTime.UtcNow;
                }
            }

            await db.SaveChangesAsync();

            return labResult.Id;
        }



        public async Task<SampleNotificationDetails?> GetSampleNotificationDetailsAsync(Guid sampleId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.Samples
                .AsNoTracking()
                .Where(s => s.Id == sampleId)
                .Select(s => new SampleNotificationDetails
                {
                    SampleId = s.Id,
                    CaseRecordId = s.CaseRecordId,
                    FacilityId = s.CaseRecord.FacilityId
                })
                .FirstOrDefaultAsync();
        }




        public async Task<LabResultNotificationDetails?> GetLabResultNotificationDetailsAsync(
    Guid caseRecordLabTestId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecordLabTests
                .AsNoTracking()
                .Where(x => x.Id == caseRecordLabTestId)
                .Select(x => new LabResultNotificationDetails
                {
                    CaseRecordLabTestId = x.Id,
                    CaseRecordId = x.CaseRecordId,
                    FacilityId = x.CaseRecord.FacilityId
                })
                .FirstOrDefaultAsync();
        }




        public async Task<LabReportDetails?> GetLabReportByIdAsync(Guid labResultId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.LabResults
                .AsNoTracking()
                .Where(r => r.Id == labResultId)
                .Select(r => new LabReportDetails
                {
                    LabResultId = r.Id,

                    CaseRecordLabTestId = r.CaseRecordLabTestId,

                    CaseRecordId = r.CaseRecordLabTest.CaseRecordId,

                    PatientName = r.CaseRecordLabTest.CaseRecord.PatientName,

                    Phone = r.CaseRecordLabTest.CaseRecord.Phone,

                    DiseaseName = r.CaseRecordLabTest.CaseRecord.DiseaseName,

                    FacilityName = r.CaseRecordLabTest.CaseRecord.Facility.FacilityName,

                    SampleTypeName = r.CaseRecordLabTest.Sample.SampleType.Name,

                    SampleBarcode = r.CaseRecordLabTest.Sample.Barcode,

                    LabTestName = r.CaseRecordLabTest.LabTest.Name,

                    ResultValue = r.ResultValue,

                    ResultStatus = r.ResultStatus,

                    EnteredAt = r.EnteredAt,

                    IsVerified = r.IsVerified,

                    Remarks = r.Remarks,

                    ReportLink = r.ReportLink
                })
                .FirstOrDefaultAsync();
        }


        public async Task<CaseEditDetails?> GetCaseForEditAsync(int id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CaseEditDetails
                {
                    Id = c.Id,

                    PatientName = c.PatientName,

                    Phone = c.Phone,

                    DiseaseName = c.DiseaseName,

                    AddressOfPatient = c.AddressOfPatient,

                    OnsetDate = c.OnsetDate,

                    DateReported = c.DateReported,

                    Notes = c.ClinicalNotes,

                    Status = c.Status,

                    FacilityId = c.FacilityId,

                    SelectedSymptomIds = c.CaseRecordSymptoms
                        .Select(x => x.SymptomId)
                        .ToList(),

                    SelectedSampleTypeIds = c.Samples
                        .Select(x => x.SampleTypeId)
                        .Distinct()
                        .ToList(),

                    SelectedLabTestIds = c.LabTests
                        .Select(x => x.LabTestId)
                        .Distinct()
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }


        public async Task<CaseLabReportDto?> GetCaseLabReportByCaseIdAsync(
            int caseRecordId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecords
                .AsNoTracking()
                .Where(c => c.Id == caseRecordId)
                .Select(c => new CaseLabReportDto
                {
                    CaseRecordId = c.Id,

                    PatientName = c.PatientName,

                    Phone = c.Phone,

                    DiseaseName = c.DiseaseName,

                    FacilityName = c.Facility.FacilityName,

                    SampleType = c.Samples
                        .Select(s => s.SampleType.Name)
                        .FirstOrDefault() ?? "-",

                    Barcode = c.Samples
                        .Select(s => s.Barcode)
                        .FirstOrDefault() ?? "-",

                    CollectedAt = c.Samples
                        .Select(s => (DateTime?)s.CollectedAt)
                        .FirstOrDefault(),

                    IsVerified = c.LabTests
                        .SelectMany(t => t.LabResults)
                        .All(r => r.IsVerified),

                    Tests = c.LabTests
                        .Select(t => new CaseLabReportTestDto
                        {
                            LabTestName = t.LabTest.Name,

                            ResultStatus = t.LabResults
                                .OrderByDescending(r => r.EnteredAt)
                                .Select(r => r.ResultStatus.ToString())
                                .FirstOrDefault() ?? "Pending",

                            ResultValue = t.LabResults
                                .OrderByDescending(r => r.EnteredAt)
                                .Select(r => r.ResultValue)
                                .FirstOrDefault(),

                            Remarks = t.LabResults
                                .OrderByDescending(r => r.EnteredAt)
                                .Select(r => r.Remarks)
                                .FirstOrDefault(),

                            TestedAt = t.LabResults
                                .OrderByDescending(r => r.EnteredAt)
                                .Select(r => (DateTime?)r.EnteredAt)
                                .FirstOrDefault(),

                            IsVerified = t.LabResults
                                .OrderByDescending(r => r.EnteredAt)
                                .Select(r => r.IsVerified)
                                .FirstOrDefault()
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }



        public async Task<bool> AreAllLabTestsVerifiedForCaseAsync(int caseRecordId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            var labTests = await db.CaseRecordLabTests
                .AsNoTracking()
                .Where(t => t.CaseRecordId == caseRecordId)
                .Select(t => new
                {
                    HasVerifiedResult = t.LabResults
                        .Any(r => r.IsVerified)
                })
                .ToListAsync();

            return labTests.Any() &&
                   labTests.All(t => t.HasVerifiedResult);
        }



        public async Task<bool> HasAnyPositiveLabResultForCaseAsync(int caseRecordId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.CaseRecordLabTests
                .AsNoTracking()
                .Where(t => t.CaseRecordId == caseRecordId)
                .SelectMany(t => t.LabResults)
                .AnyAsync(r =>
                    r.IsVerified &&
                    r.ResultStatus == LabResultStatus.Positive);
        }

        public async Task<List<NotificationDto>> GetNotificationsForUserAsync(string userId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            return await db.NotificationRecipients
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Notification.Timestamp)
                .Select(r => new NotificationDto
                {
                    Id = r.Notification.Id,
                    CaseRecordId = r.Notification.CaseRecordId,
                    FacilityId = r.Notification.FacilityId,
                    DiseaseName = r.Notification.DiseaseName,
                    FacilityName = r.Notification.CaseRecord.Facility.FacilityName,
                    Type = r.Notification.Type.ToString(),
                    Timestamp = r.Notification.Timestamp,
                    IsChecked = r.IsRead,
                    LabResultId = r.Notification.LabResultId
                })
                .ToListAsync();
        }

        public async Task MarkNotificationsReadForUserAsync(
      string userId,
      List<int> notificationIds)
        {
            using var db = await _dbFactory.CreateDbContextAsync();

            var recipients = await db.NotificationRecipients
                .Where(r =>
                    r.UserId == userId &&
                    notificationIds.Contains(r.NotificationId))
                .ToListAsync();

            foreach (var recipient in recipients)
            {
                recipient.IsRead = true;
                recipient.ReadAt = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();
        }
    }

}
