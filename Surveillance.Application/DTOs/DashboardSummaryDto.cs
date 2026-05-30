using System;
using System.Collections.Generic;
using System.Text;
using UPHC.SurveillanceDashboard.Models;

namespace Surveillance.Application.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalPatients { get; set; }      //  TotalCases 

        public List<OutbreakDto> ConfirmedOutbreaks { get; set; } = [];

        public List<OutbreakDto> SuspectedClusters { get; set; } = [];

        public List<FacilityStatDto> FacilityStats { get; set; } = [];

        public List<TrendPointDto> TrendData { get; set; } = [];

        public List<DiseaseDistributionDto> DiseaseDistribution { get; set; } = [];

        public List<FacilityChartDto> FacilityChart { get; set; } = [];
    }
}
