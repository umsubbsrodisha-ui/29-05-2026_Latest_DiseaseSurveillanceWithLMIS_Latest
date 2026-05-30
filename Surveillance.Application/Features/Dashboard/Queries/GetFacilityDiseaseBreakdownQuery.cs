using System;
using System.Collections.Generic;
using System.Text;

using MediatR;
using Surveillance.Application.DTOs;

namespace Surveillance.Application.Features.Dashboard.Queries
{
    public class GetFacilityDiseaseBreakdownQuery : IRequest<List<FacilityDiseaseBreakdownDto>>
    {
        public int Days { get; set; } = 14;
    }
}
