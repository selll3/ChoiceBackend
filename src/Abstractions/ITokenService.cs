using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Choice_Ym.Abstractions
{
    public interface ITokenService
    {
        JwtSecurityToken GetToken(List<Claim> authClaims);
    }
}