//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Application.Features.Cases.Queries
//{
//    internal class GetAllSampleTypesHandler
//    {
//    }
//}

using MediatR;
using Surveillance.Application.Interfaces.Repositories;
using Surveillance.Domain.Entities;

namespace Surveillance.Application.Features.MasterData.Queries
{
    public class GetAllSampleTypesHandler
        : IRequestHandler<GetAllSampleTypesQuery, List<SampleType>>
    {
        private readonly IRepository _repository;

        public GetAllSampleTypesHandler(
            IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SampleType>> Handle(
            GetAllSampleTypesQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository
                .GetAllSampleTypesAsync();
        }
    }
}
