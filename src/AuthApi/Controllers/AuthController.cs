using AuthApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Uma chave secreta usada para assinar o token JWT (deve ser armazenada de forma segura)
        private readonly string _secretKey = "your_secret_key_here";

        // Endpoint para autenticação
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            // Validação básica do usuário (substitua pela validação real)
            if (user.Username == "test" && user.Password == "password")
            {
                var token = GenerateJwtToken(user.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        // Geração do token JWT
        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}