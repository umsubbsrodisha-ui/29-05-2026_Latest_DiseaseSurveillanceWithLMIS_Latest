using MediatR;
using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Samples.Commands
{
    public class UpdateSampleStatusCommand : IRequest<bool>
    {
        public Guid SampleId { get; set; }

        public SampleStatus NewStatus { get; set; }

        public string? Barcode { get; set; }

        public string? CollectedBy { get; set; }

        public string? CollectionNotes { get; set; }

        public string? DispatchReferenceNo { get; set; }
    }
}
