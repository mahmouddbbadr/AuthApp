using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using AuthApp.Domain.Entites;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace AuthApp.Application.Servises
{
    public static class CreateToken
    {
        public static async Task<string> CreateJWTToken(UserManager<AppUser> userManager, IConfiguration configuration, AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(2),
                signingCredentials: signingCredentials,
                claims: claims
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;

        }
    }
}
