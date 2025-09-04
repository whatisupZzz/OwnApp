using BuildingBlocks.JWT.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.JWT
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserIdentity()
        {
            var claims = _context.HttpContext?.User?.Claims;
            foreach (var c in claims ?? Enumerable.Empty<Claim>())
            {
                Console.WriteLine($"{c.Type}: {c.Value}");
            }
            return _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }
    }

}
