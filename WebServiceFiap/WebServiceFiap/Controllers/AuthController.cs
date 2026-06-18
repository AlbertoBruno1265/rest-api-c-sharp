using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebServiceFiap.Models.Auth;
using WebServiceFiap.Services;

namespace WebServiceFiap.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsuarioService _usuarioService;

        public AuthController(
            IConfiguration configuration,
            UsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var usuario = _usuarioService.Authenticate(request.Email, request.Senha);

            if (usuario == null)
                return Unauthorized("E-mail ou senha invalidos.");

            var expiraEm = DateTime.UtcNow.AddHours(2);
            var token = GenerateToken(usuario.Nome, usuario.Email, usuario.Funcao, expiraEm);

            return Ok(new LoginResponse
            {
                Token = token,
                ExpiraEm = expiraEm
            });
        }

        private string GenerateToken(
            string nome,
            string email,
            string funcao,
            DateTime expiraEm)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var secretKey = _configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey ?? string.Empty));
            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, funcao)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiraEm,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
