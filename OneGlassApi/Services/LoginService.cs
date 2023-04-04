using Microsoft.IdentityModel.Tokens;
using OneGlassApi.DTO;
using OneGlassApi.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OneGlassApi.Services
{
    public class LoginService : ILoginService
    {
        public string GenearteJwtToken(string username, string password)
        {
            if (VerifyLoginCredential(username, password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisasecretkey@123"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: "ABCXYZ",
                    audience: "http://localhost:51398",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signinCredentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return token;
            }
            return null;
        }

        private bool VerifyLoginCredential(string username, string password)
        {
            if (username.Equals("soroush") && password.Equals("soroush123"))
                return true;

            return false;
        }
    }
}
