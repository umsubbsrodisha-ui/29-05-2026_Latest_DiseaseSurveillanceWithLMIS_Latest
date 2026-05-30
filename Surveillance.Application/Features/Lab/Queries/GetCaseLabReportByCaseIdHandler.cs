using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;

namespace Surveillance.Application.Features.Lab.Queries
{
    public class GetCaseLabReportByCaseIdHandler
        : IRequestHandler<GetCaseLabReportByCaseIdQuery, CaseLabReportDto?>
    {
        private readonly IRepository _repository;

        public GetCaseLabReportByCaseIdHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CaseLabReportDto?> Handle(
            GetCaseLabReportByCaseIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetCaseLabReportByCaseIdAsync(
                request.CaseRecordId);
        }
    }
}