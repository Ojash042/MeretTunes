using System.Security.Claims;
using MeretTune.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace MeretTune.Factory; 

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser> {
	public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor) {
	}

	protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user) {
		var identity = await base.GenerateClaimsAsync(user);
		identity.AddClaim(new Claim("UserName", user.UserName));
		return identity;
	}
	
}