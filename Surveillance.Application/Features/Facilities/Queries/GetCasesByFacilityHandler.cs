using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Facilities.Queries
{
    public class GetCasesByFacilityHandler : IRequestHandler<GetCasesByFacilityQuery, FacilityCasesDto>
    {
        private readonly IRepository _repository;

        public GetCasesByFacilityHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<FacilityCasesDto> Handle(GetCasesByFacilityQuery request, CancellationToken cancellationToken)
        {


            // Run queries in parallel
            var facilityTask =
                _repository.GetFacilityByIdAsync(request.FacilityId);

            var casesTask =
                _repository.GetCasesByFacilityAsync(
                    request.FacilityId,
                    request.Page,
                    request.PageSize);

            var totalCountTask =
                _repository.GetCasesCountByFacilityAsync(
                    request.FacilityId);

            await Task.WhenAll(
                facilityTask,
                casesTask,
                totalCountTask);

            var facility = await facilityTask;

            var cases = await casesTask;

            var totalCount = await totalCountTask;

            return new FacilityCasesDto
            {
                FacilityName =
                    facility?.FacilityName ?? "Unknown",

                FacilityAddress =
                    facility?.FacilityAddress ?? "",

                Cases = cases.Select(c => new CaseListDto
                {
                    Id = c.Id,
                    CreatedDate = c.CreatedDate,
                    DiseaseName = c.DiseaseName,
                    PatientName = c.PatientName
                }).ToList(),

                TotalCount = totalCount,

                CurrentPage = request.Page,

                PageSize = request.PageSize
            };
            //// Get facility info
            //var facility = await _repository.GetFacilityByIdAsync(request.FacilityId);


            //var cases = await _repository.GetCasesByFacilityAsync(
            //    request.FacilityId,
            //    request.Page,
            //    request.PageSize);

            //var totalCount = await _repository
            //    .GetCasesCountByFacilityAsync(request.FacilityId);

            //return new FacilityCasesDto
            //{
            //    FacilityName = facility?.FacilityName ?? "Unknown",
            //    FacilityAddress = facility?.FacilityAddress ?? "",
            //    Cases = cases.Select(c => new CaseListDto
            //    {
            //        Id = c.Id,
            //        CreatedDate = c.CreatedDate,
            //        DiseaseName = c.DiseaseName,
            //        PatientName = c.PatientName
            //    }).ToList(),
            //    TotalCount = totalCount,
            //    CurrentPage = request.Page,
            //    PageSize = request.PageSize
            //};
        }
    }
}
