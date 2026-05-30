using System;
using System.Collections.Generic;
using System.Text;


namespace Surveillance.Application.DTOs;

public class FacilityDto
{
    public int Id { get; set; }

    public string FacilityName { get; set; } = string.Empty;

    public string FacilityAddress { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
}