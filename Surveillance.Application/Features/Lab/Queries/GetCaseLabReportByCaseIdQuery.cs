using MediatR;
using Surveillance.Application.DTOs;

namespace Surveillance.Application.Features.Lab.Queries
{
    public class GetCaseLabReportByCaseIdQuery : IRequest<CaseLabReportDto?>
    {
        public int CaseRecordId { get; set; }
    }
}