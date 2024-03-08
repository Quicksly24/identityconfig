using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication4.auth
{
    public class Gentoken
    {
        public string gentoken(string username, string password)
        {


            var signcredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication-")), SecurityAlgorithms.HmacSha256);

            var claims = new[]{
            new Claim(ClaimTypes.Name,username),
            new Claim(JwtRegisteredClaimNames.Email,password),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
             };

            //List<Claim> claims1 = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name,username),
            //    new Claim(ClaimTypes.Dsa,Guid.NewGuid().ToString()),
            //    new Claim(ClaimTypes.Email,password),

            //};


            var securitytoken = new JwtSecurityToken(
                issuer: "solo",
                audience: "post",        
                expires: DateTime.UtcNow.AddMinutes(30),
                claims: claims,
                signingCredentials: signcredentials

            );

            return new JwtSecurityTokenHandler().WriteToken(securitytoken);

        }

    }
}
