using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Surveillance.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int? FacilityId { get; set; }  //many to one with facility table 
        public Facility? Facility { get; set; }
    }
}
