using System;
using System.Collections.Generic;
using System.Text;

using MediatR;
using Surveillance.Application.DTOs;

namespace Surveillance.Application.Features.Facilities.Queries
{
    public class GetCasesByFacilityQuery : IRequest<FacilityCasesDto>
    {
        public int FacilityId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
