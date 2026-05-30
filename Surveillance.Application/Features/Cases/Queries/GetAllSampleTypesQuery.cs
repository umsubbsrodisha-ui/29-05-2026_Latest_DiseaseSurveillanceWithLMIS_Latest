//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Application.Features.Cases.Queries
//{
//    internal class GetAllSampleTypesQuery
//    {
//    }
//}
using MediatR;
using Surveillance.Domain.Entities;

namespace Surveillance.Application.Features.MasterData.Queries
{
    public class GetAllSampleTypesQuery : IRequest<List<SampleType>>
    {
    }
}