using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Features.Cases.Queries;
using Surveillance.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Cases.Queries
{
    internal class GetCommunicableRecordsQueryHandler
    {
    }
}


public class GetCommunicableRecordsQueryHandler: IRequestHandler<GetCommunicableRecordsQuery,List<CommunicableRecordDto>>
{
    private readonly IRepository _repository;


public GetCommunicableRecordsQueryHandler(
    IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CommunicableRecordDto>> Handle(
        GetCommunicableRecordsQuery request,
        CancellationToken cancellationToken)
    {
        var records =
            await _repository.GetCommunicableRecordsAsync(
                request.PageNumber,
                request.PageSize);

        return records.Select(r => new CommunicableRecordDto
        {
            Id = r.Id,

            PatientName = r.PatientName,

            FacilityName = r.FacilityName,

            OnsetDate = r.OnsetDate
        }).ToList();
    }


}
