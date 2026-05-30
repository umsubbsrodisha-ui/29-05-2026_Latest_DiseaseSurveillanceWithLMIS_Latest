//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Application.Features.Cases.Queries
//{
//    internal class GetAllSymptomsHandler
//    {
//    }
//}

using MediatR;
using Surveillance.Application.Interfaces.Repositories;
using Surveillance.Domain.Entities;

namespace Surveillance.Application.Features.MasterData.Queries
{
    public class GetAllSymptomsHandler
        : IRequestHandler<GetAllSymptomsQuery, List<Symptom>>
    {
        private readonly IRepository _repository;

        public GetAllSymptomsHandler(
            IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Symptom>> Handle(
            GetAllSymptomsQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository
                .GetAllSymptomsAsync();
        }
    }
}
