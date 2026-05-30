using MediatR;
using Surveillance.Application.DTOs;

namespace Surveillance.Application.Features.Lab.Queries
{
    public class GetLabReportByIdQuery : IRequest<LabReportDto?>
    {
        public Guid LabResultId { get; set; }
    }
}