using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GB_Corporation.Helpers
{
    public class JwtService
    {
        private string secureKey = "rhjkdSFdsSdhjsEdjkQdjskTdj";
        private readonly IRoleRepository _roleRepository;

        public JwtService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public string Generate(Employee user, int roleId)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(user.Id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            
            var role = _roleRepository.GetById(roleId).Title;
            payload.Add("roles", role);
            
            var securityToken = new JwtSecurityToken(header, payload);
            
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
