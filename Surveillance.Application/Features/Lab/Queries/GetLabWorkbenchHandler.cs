using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;

namespace Surveillance.Application.Features.Lab.Queries
{
    public class GetLabWorkbenchHandler
        : IRequestHandler<GetLabWorkbenchQuery, List<LabWorkbenchDto>>
    {
        private readonly IRepository _repository;

        public GetLabWorkbenchHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LabWorkbenchDto>> Handle(
            GetLabWorkbenchQuery request,
            CancellationToken cancellationToken)
        {
            var items = await _repository
                .GetLabWorkbenchByFacilityAsync(request.FacilityId);

            return items.Select(x => new LabWorkbenchDto
            {
                CaseRecordLabTestId = x.CaseRecordLabTestId,
                CaseRecordId = x.CaseRecordId,
                PatientName = x.PatientName,
                Phone = x.Phone,
                DiseaseName = x.DiseaseName,
                FacilityId = x.FacilityId,
                FacilityName = x.FacilityName,
                SampleId = x.SampleId,
                SampleBarcode = x.SampleBarcode,
                SampleTypeName = x.SampleTypeName,
                SampleStatus = x.SampleStatus.ToString(),
                LabTestId = x.LabTestId,
                LabTestName = x.LabTestName,
                TestedAt = x.TestedAt,
                ReportPath = x.ReportPath,
                LatestResultValue = x.LatestResultValue,
                LatestResultStatus = x.LatestResultStatus?.ToString() ?? "Pending",
                LatestResultEnteredAt = x.LatestResultEnteredAt,
                LatestResultIsVerified = x.LatestResultIsVerified,
                LatestResultRemarks = x.LatestResultRemarks
            }).ToList();
        }
    }
}