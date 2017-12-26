using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Edi.LightBlog.Core
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            principal.Identities.First().AddClaim(new Claim("now", DateTime.UtcNow.ToString()));
            return Task.FromResult(principal);
        }
    }
}