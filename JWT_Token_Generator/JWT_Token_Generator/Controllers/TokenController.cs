using JWT_Token_Generator.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_Token_Generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> GenerateToken([FromBody] Token token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.SecretKey));
            var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, token.Username),
                new Claim(ClaimTypes.Role, token.Roles[0]),
                new Claim("tenantId", "T5")
            };
            foreach (var item in token.Roles)
            {
                claims.Append(new Claim(ClaimTypes.Role, item));
                
            }
            var jwtToken = new JwtSecurityToken(
                    issuer: token.Issuer.ToString(),
                    audience: token.Audience.ToString(),
                    expires: token.ExpirationTime,
                    claims: claims,
                    signingCredentials : credintials
                );
            
            return Ok(new JwtSecurityTokenHandler().WriteToken(jwtToken));
        } 
    }
}
