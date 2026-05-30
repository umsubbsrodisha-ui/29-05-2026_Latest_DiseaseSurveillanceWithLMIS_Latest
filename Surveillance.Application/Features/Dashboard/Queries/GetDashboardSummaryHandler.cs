using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;
using Surveillance.Domain.Entities;
using UPHC.SurveillanceDashboard.Models;

namespace Surveillance.Application.Features.Dashboard.Queries
{
    public class GetDashboardSummaryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryDto>
    {
        private readonly IRepository _repository;

        public GetDashboardSummaryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardSummaryDto> Handle(
            GetDashboardSummaryQuery request,
            CancellationToken cancellationToken)
        {
            var fromDate = DateTime.UtcNow.AddDays(-request.Days);

            // Run all dashboard queries in parallel
            var totalPatientsTask =
                _repository.GetCommunicableCaseCountAsync(fromDate);

            var confirmedOutbreaksTask =
                _repository.GetConfirmedOutbreaksAsync(fromDate);

            var suspectedClustersTask =
                _repository.GetSuspectedClustersAsync(fromDate);

            var facilityStatsTask =
                _repository.GetFacilityStatsAsync(fromDate);

            var trendDataTask =
                _repository.GetTrendDataAsync(fromDate);

            var diseaseDistributionTask =
                _repository.GetDiseaseDistributionAsync(fromDate);

            var facilityChartTask =
                _repository.GetFacilityChartDataAsync(fromDate);

            await Task.WhenAll(
                totalPatientsTask,
                confirmedOutbreaksTask,
                suspectedClustersTask,
                facilityStatsTask,
                trendDataTask,
                diseaseDistributionTask,
                facilityChartTask);

            return new DashboardSummaryDto
            {
                TotalPatients = await totalPatientsTask,

                ConfirmedOutbreaks =
                    await confirmedOutbreaksTask,

                SuspectedClusters =
                    await suspectedClustersTask,

                FacilityStats =
                    await facilityStatsTask,

                TrendData =
                    await trendDataTask,

                DiseaseDistribution =
                    await diseaseDistributionTask,

                FacilityChart =
                    await facilityChartTask
            };
        }
    


        //public async Task<DashboardSummaryDto> Handle(
        //    GetDashboardSummaryQuery request,
        //    CancellationToken cancellationToken)
        //{
        //    var fromDate = DateTime.UtcNow.AddDays(-request.Days);

        //    // Optimized repository queries
        //    var totalPatients =
        //        await _repository.GetCommunicableCaseCountAsync(fromDate);

        //    var confirmedOutbreaks =
        //        await _repository.GetConfirmedOutbreaksAsync(fromDate);

        //    var suspectedClusters =
        //        await _repository.GetSuspectedClustersAsync(fromDate);

        //    var facilities =
        //       await _repository.GetAllFacilitiesAsync();

        //    return new DashboardSummaryDto
        //    {
        //        TotalPatients = totalPatients,

        //        ConfirmedOutbreaks = confirmedOutbreaks,

        //        SuspectedClusters = suspectedClusters,

        //        FacilityStats = facilities
        //            .Select(f => new FacilityStatDto
        //            {
        //                Id = f.Id,
        //                Name = f.FacilityName,
        //                TotalCases = f.CaseRecords?
        //                    .Count(c =>
        //                        c.IsCommunicable &&
        //                        c.OnsetDate >= fromDate) ?? 0
        //            })
        //            .ToList(),

        //        TrendData = GenerateTrendData(facilities, fromDate),

        //        DiseaseDistribution = GetTopDiseases(facilities, fromDate),

        //        FacilityChart = GetFacilityChartData(facilities, fromDate)
        //    };
        //}

        private List<TrendPointDto> GenerateTrendData(
            List<Facility> facilities,
            DateTime fromDate)
        {
            var allCases = facilities
                .SelectMany(f => f.CaseRecords ?? []);

            var trend = new List<TrendPointDto>();

            for (int i = 13; i >= 0; i--)
            {
                var date = DateTime.UtcNow.Date.AddDays(-i);

                trend.Add(new TrendPointDto
                {
                    Label = date.ToString("dd MMM"),

                    ConfirmedCount = allCases.Count(c =>
                        c.Status == Surveillance.Domain.Enums.CaseStatus.Confirmed &&
                        c.OnsetDate.Date == date),

                    SuspectedCount = allCases.Count(c =>
                        c.Status == Surveillance.Domain.Enums.CaseStatus.Suspected &&
                        c.OnsetDate.Date == date)
                });
            }

            return trend;
        }

        private List<DiseaseDistributionDto> GetTopDiseases(
            List<Facility> facilities,
            DateTime fromDate)
        {
            var allCases = facilities
                .SelectMany(f => f.CaseRecords ?? [])
                .Where(c =>
                    c.IsCommunicable &&
                    c.OnsetDate >= fromDate);

            return allCases
                .GroupBy(c => c.DiseaseName)
                .Select(g => new DiseaseDistributionDto
                {
                    DiseaseName = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(6)
                .ToList();
        }

        private List<FacilityChartDto> GetFacilityChartData(
            List<Facility> facilities,
            DateTime fromDate)
        {
            return facilities
                .Select(f => new FacilityChartDto
                {
                    FacilityName = f.FacilityName,

                    Count = f.CaseRecords?
                        .Count(c =>
                            c.IsCommunicable &&
                            c.OnsetDate >= fromDate) ?? 0
                })
                .OrderByDescending(x => x.Count)
                .Take(8)
                .ToList();
        }
    }
}

































//using MediatR;
//using Surveillance.Application.DTOs;
//using Surveillance.Application.Interfaces.Repositories;
//using Surveillance.Domain.Entities;
//using Surveillance.Domain.Enums;
//using UPHC.SurveillanceDashboard.Models;

//namespace Surveillance.Application.Features.Dashboard.Queries
//{
//    public class GetDashboardSummaryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryDto>
//    {
//        private readonly IRepository _repository;

//        public GetDashboardSummaryHandler(IRepository repository)
//        {
//            _repository = repository;
//        }

//        public async Task<DashboardSummaryDto> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
//        {
//            var fromDate = DateTime.UtcNow.AddDays(-request.Days);

//            // Fetch domain entities from repository
//            var cases = await _repository.GetCommunicableCasesFromDateAsync(fromDate);
//            var facilities = await _repository.GetAllFacilitiesAsync();

//            // Map Domain → DTOs here in Application layer
//            return new DashboardSummaryDto
//            {
//                TotalPatients = cases.Count,

//                ConfirmedOutbreaks = cases
//                    .Where(c => c.Status == CaseStatus.Confirmed)
//                    .GroupBy(c => new { c.DiseaseName, FacilityName = c.Facility?.FacilityName ?? "Unknown" })
//                    .Where(g => g.Count() >= 3)
//                    .Select(g => new OutbreakDto
//                    {
//                        DiseaseName = g.Key.DiseaseName,
//                        FacilityName = g.Key.FacilityName,
//                        Count = g.Count(),
//                        FirstCaseDate = g.Min(x => x.OnsetDate)
//                    }).ToList(),

//                SuspectedClusters = cases
//                    .Where(c => c.Status == CaseStatus.Suspected)
//                    .GroupBy(c => new { c.DiseaseName, FacilityName = c.Facility?.FacilityName ?? "Unknown" })
//                    .Where(g => g.Count() >= 5)
//                    .Select(g => new OutbreakDto
//                    {
//                        DiseaseName = g.Key.DiseaseName,
//                        FacilityName = g.Key.FacilityName,
//                        Count = g.Count(),
//                        FirstCaseDate = g.Min(x => x.OnsetDate)
//                    }).ToList(),

//                FacilityStats = facilities.Select(f => new FacilityStatDto
//                {
//                    Id = f.Id,
//                    Name = f.FacilityName,
//                    TotalCases = f.CaseRecords?.Count(c => c.IsCommunicable) ?? 0
//                }).ToList(),

//                TrendData = GenerateTrendData(cases, fromDate),
//                DiseaseDistribution = GetTopDiseases(cases),
//                FacilityChart = GetFacilityChartData(facilities, fromDate)
//            };
//        }

//        private List<TrendPointDto> GenerateTrendData(List<CaseRecord> cases, DateTime fromDate)
//        {
//            var trend = new List<TrendPointDto>();

//            for (int i = 13; i >= 0; i--)
//            {
//                var date = DateTime.UtcNow.Date.AddDays(-i);
//                trend.Add(new TrendPointDto
//                {
//                    Label = date.ToString("dd MMM"),
//                    ConfirmedCount = cases.Count(c => c.Status == CaseStatus.Confirmed && c.OnsetDate.Date == date),
//                    SuspectedCount = cases.Count(c => c.Status == CaseStatus.Suspected && c.OnsetDate.Date == date)
//                });
//            }
//            return trend;
//        }

//        private List<DiseaseDistributionDto> GetTopDiseases(List<CaseRecord> cases)
//        {
//            return cases
//                .GroupBy(c => c.DiseaseName)
//                .Select(g => new DiseaseDistributionDto
//                {
//                    DiseaseName = g.Key,
//                    Count = g.Count()
//                })
//                .OrderByDescending(x => x.Count)
//                .Take(6)
//                .ToList();
//        }

//        private List<FacilityChartDto> GetFacilityChartData(List<Facility> facilities, DateTime fromDate)
//        {
//            return facilities
//                .Select(f => new FacilityChartDto
//                {
//                    FacilityName = f.FacilityName,
//                    Count = f.CaseRecords?.Count(c => c.OnsetDate >= fromDate && c.IsCommunicable) ?? 0
//                })
//                .OrderByDescending(x => x.Count)
//                .Take(8)
//                .ToList();
//        }
//    }
//}