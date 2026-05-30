using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;

namespace Surveillance.Application.Features.Lab.Queries
{
    public class GetLabReportByIdHandler
        : IRequestHandler<GetLabReportByIdQuery, LabReportDto?>
    {
        private readonly IRepository _repository;

        public GetLabReportByIdHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<LabReportDto?> Handle(
            GetLabReportByIdQuery request,
            CancellationToken cancellationToken)
        {
            var report = await _repository
                .GetLabReportByIdAsync(request.LabResultId);

            if (report == null)
                return null;

            return new LabReportDto
            {
                LabResultId = report.LabResultId,

                CaseRecordLabTestId = report.CaseRecordLabTestId,

                CaseRecordId = report.CaseRecordId,

                PatientName = report.PatientName,

                Phone = report.Phone,

                DiseaseName = report.DiseaseName,

                FacilityName = report.FacilityName,

                SampleTypeName = report.SampleTypeName,

                SampleBarcode = report.SampleBarcode,

                LabTestName = report.LabTestName,

                ResultValue = report.ResultValue,

                ResultStatus = report.ResultStatus.ToString(),

                EnteredAt = report.EnteredAt,

                IsVerified = report.IsVerified,

                Remarks = report.Remarks,

                ReportLink = report.ReportLink
            };
        }
    }
}