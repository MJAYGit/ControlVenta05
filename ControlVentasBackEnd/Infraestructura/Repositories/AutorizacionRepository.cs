using System;
using ControlVentasBackEnd.Domain;
using ControlVentasBackEnd.Infraestructura.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;


namespace ControlVentasBackEnd.Infraestructura.Repositories
{
    public class AutorizacionRepository : IAutorizacionRepository
    {
        private IConfiguration _config;

        public AutorizacionRepository(VentaDbContext context, IConfiguration config)
        {
            _config = config;
        }
        public string GenerarToken(Autorizacion aUsuario)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, aUsuario.UsuarioNombre)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
                );

            string sToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return sToken;
        }
    }
}
