using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Surveillance.Domain.Entities;

namespace Surveillance.Infrastructure.Identity
{
    public class AdditionalUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(
            ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            if (user.FacilityId.HasValue)
            {
                identity.AddClaim(
                    new Claim(
                        "FacilityId",
                        user.FacilityId.Value.ToString()));
            }

            return identity;
        }
    }
}