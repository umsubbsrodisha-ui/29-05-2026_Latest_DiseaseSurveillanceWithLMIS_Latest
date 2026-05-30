using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class FacilityCasesDto
    {
        public string FacilityName { get; set; } = string.Empty;
        public string FacilityAddress { get; set; } = string.Empty;
        public List<CaseListDto> Cases { get; set; } = new();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}