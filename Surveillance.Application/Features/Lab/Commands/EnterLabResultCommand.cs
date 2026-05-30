using MediatR;
using Surveillance.Domain.Enums;

namespace Surveillance.Application.Features.Lab.Commands
{
    public class EnterLabResultCommand : IRequest<bool>
    {
        public Guid CaseRecordLabTestId { get; set; }

        public string ResultValue { get; set; } = string.Empty;

        public LabResultStatus ResultStatus { get; set; }

        public string? EnteredByUserId { get; set; }

        public bool IsVerified { get; set; }

        public string? VerifiedByUserId { get; set; }

        public string? Remarks { get; set; }

        public string? ReportPath { get; set; }
    }
}