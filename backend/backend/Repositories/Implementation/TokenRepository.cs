using backend.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(roles.Select(roles => new Claim(ClaimTypes.Role, roles))); // add claims and แปลง roles แต่ละตัวให้เป็น Claim ประเภท Role
            
            // Jwt security token parameter
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])); // สร้างคีย์ความปลอดภัยจากค่าที่กำหนดในไฟล์คอนฟิก
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // สร้าง credentials โดยใช้อัลกอริทึม HmacSha256 สำหรับการเข้ารหัส
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"], // token creator
                audience: configuration["Jwt:Audience"], // token receiver
                claims: claims,
                expires: DateTime.Now.AddMinutes(15), // expire after 15 min. from now
                signingCredentials: credentials);

            // Retuen Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
