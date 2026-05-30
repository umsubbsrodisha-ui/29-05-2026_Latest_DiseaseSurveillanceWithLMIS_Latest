using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Dashboard.Queries
{
    public class GetFacilityDiseaseBreakdownHandler : IRequestHandler<GetFacilityDiseaseBreakdownQuery, List<FacilityDiseaseBreakdownDto>>
    {
        private readonly IRepository _repository;

        public GetFacilityDiseaseBreakdownHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FacilityDiseaseBreakdownDto>> Handle(GetFacilityDiseaseBreakdownQuery request,CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllFacilitiesAsync();


            return data
                .GroupBy(x => new
                {
                    x.FacilityId,
                    x.FacilityName
                })
                .Select(f => new FacilityDiseaseBreakdownDto
                {
                    FacilityId = f.Key.FacilityId,

                    Diseases = f
                        .OrderByDescending(x => x.Count)
                        .Take(4)
                        .Select(x => new DiseaseCountDto
                        {
                            DiseaseName = x.DiseaseName,
                            Count = x.Count
                        })
                        .ToList()
                })
                .ToList();


        }


        //public async Task<List<FacilityDiseaseBreakdownDto>> Handle(GetFacilityDiseaseBreakdownQuery request, CancellationToken cancellationToken)
        //{
        //    var fromDate = DateTime.UtcNow.AddDays(-request.Days);
        //    var facilities = await _repository.GetAllFacilitiesAsync();

        //    return facilities.Select(f => new FacilityDiseaseBreakdownDto
        //    {
        //        FacilityId = f.Id,
        //        Diseases = f.CaseRecords?
        //            .Where(c => c.IsCommunicable && c.OnsetDate >= fromDate)
        //            .GroupBy(c => c.DiseaseName)
        //            .Select(g => new DiseaseCountDto
        //            {
        //                DiseaseName = g.Key,
        //                Count = g.Count()
        //            })
        //            .OrderByDescending(d => d.Count)
        //            .Take(4)
        //            .ToList() ?? new()
        //    }).ToList();
        //}
    }
}
