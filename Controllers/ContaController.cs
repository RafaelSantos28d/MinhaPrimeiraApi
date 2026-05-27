using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinhaPrimeiraApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinhaPrimeiraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if(login.Login == "admin" && login.Senha == "admin")
            {
                var token = GerarToken();
                return Ok(new { token = token });
            }
            return BadRequest(new { mensagem = "Credencias invalidas.Por favor verifique seu usuario e senha."});
        }
        private string GerarToken()
        {
            string chaveSecrta = "6ef2c204-f262-40aa-be9f-1385bbc15137";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecrta));
            var credencial = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("login","admin"),
                new Claim("nome","Administrador do sistema")

            };
            var token = new JwtSecurityToken(
                issuer: "sua_empresa",
                audience: "sua_aplicacao",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencial  
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
