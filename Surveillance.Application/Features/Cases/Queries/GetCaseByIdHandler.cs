using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;


namespace Surveillance.Application.Features.Cases.Queries
{
    public class GetCaseByIdHandler : IRequestHandler<GetCaseByIdQuery, CaseDetailsDto?>
    {
        private readonly IRepository _repository;

        public GetCaseByIdHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CaseDetailsDto?> Handle(
            GetCaseByIdQuery request,
            CancellationToken cancellationToken)
        {
            var caseRecord = await _repository.GetCaseByIdAsync(request.Id);

            if (caseRecord == null)
                return null;

            return new CaseDetailsDto
            {
                Id = caseRecord.Id,
                PatientName = caseRecord.PatientName,
                Phone = caseRecord.Phone,
                DiseaseName = caseRecord.DiseaseName,
                AddressOfPatient = caseRecord.AddressOfPatient,
                Notes = caseRecord.Notes,

                OnsetDate = caseRecord.OnsetDate,
                DateReported = caseRecord.DateReported,
                CreatedDate = caseRecord.CreatedDate,
                LabConfirmedDate = caseRecord.LabConfirmedDate,

                IsCommunicable = caseRecord.IsCommunicable,
                CaseStatus = caseRecord.Status.ToString(),

                FacilityId = caseRecord.FacilityId,
                FacilityName = caseRecord.FacilityName,

                UserId = caseRecord.UserId,
                CreatedByName = caseRecord.CreatedByName,

                Symptoms = caseRecord.Symptoms,

                Samples = caseRecord.Samples.Select(s => new CaseSampleDto
                {
                    SampleId = s.SampleId,
                    SampleType = s.SampleType,
                    SampleStatus = s.SampleStatus.ToString(),
                    Barcode = s.Barcode,
                    DispatchReferenceNo = s.DispatchReferenceNo,
                    CollectedAt = s.CollectedAt,
                    CollectedBy = s.CollectedBy,
                    CollectionNotes = s.CollectionNotes,
                    DispatchedAt = s.DispatchedAt,
                    ReceivedAtLabAt = s.ReceivedAtLabAt,
                    ProcessingFacilityId = s.ProcessingFacilityId,
                    ProcessingFacilityName = s.ProcessingFacilityName
                }).ToList(),

                LabTests = caseRecord.LabTests.Select(t => new CaseLabTestDto
                {
                    CaseRecordLabTestId = t.CaseRecordLabTestId,
                    LabTestName = t.LabTestName,
                    LabResultStatus = string.IsNullOrWhiteSpace(t.ResultValue)
                        ? "Pending"
                        : t.LabResultStatus.ToString(),
                    TestedAt = t.TestedAt,
                    ResultValue = t.ResultValue,
                    Remarks = t.Remarks,
                    ReportPath = t.ReportPath,
                    SampleId = t.SampleId,
                    SampleBarcode = t.SampleBarcode,
                    LabResultId = t.LabResultId,
                    IsVerified = t.IsVerified
                }).ToList()
            };
        }
    }
}