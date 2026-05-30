//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Application.Features.Cases.Queries
//{
//    internal class GetAllSymptomsQuery
//    {
//    }
//}
using MediatR;
using Surveillance.Domain.Entities;

namespace Surveillance.Application.Features.MasterData.Queries
{
    public class GetAllSymptomsQuery : IRequest<List<Symptom>>
    {
    }
}