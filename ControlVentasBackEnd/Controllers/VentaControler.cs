using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*==================*/
using ControlVentasBackEnd.Model;
using ControlVentasBackEnd.Infraestructura.Repositories;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;


namespace ControlVentasBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class VentaControler : ControllerBase
    {
        private readonly VentaDbContext _context;
        private IConfiguration _config ;
        private readonly IVentaRepository _VentaRepository;

        public VentaControler(VentaDbContext context, IConfiguration config, IVentaRepository aVentaRepository)
        {
            _context = context;
            _config = config;
            _VentaRepository = aVentaRepository;
        }


        [Authorize]
        [HttpGet]
        //[Route("GetAllAuthor")]
        public async Task<List<Venta>> GetVentas()
        {
            var Ventas = await _VentaRepository.GetVentas();
            return Ventas;
        }

        [Authorize]
        [HttpGet(template: "{id}")]
        public async Task<Venta> GetVenta(int id)
        {
            var Venta = await _VentaRepository.GetVenta(id);
            return Venta;
        }


        [Authorize]
        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _VentaRepository.Delete(id);
            return Ok("Venta con id:" + id + " se eliminó correctamente");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddVenta(Venta aVenta)
        {
            await _VentaRepository.AddVenta( aVenta);
            return Created("api/ventas/" + aVenta.Id, aVenta);
        }

        [Authorize]
        [HttpPut(template: "{id}")]
        public async Task<IActionResult> Update(int id, Venta aVenta)
        {
            await _VentaRepository.Update(id, aVenta);
            return Ok("Venta con id:" + id + " se actualizó correctamente");
        }


        /* ========= Seguridad Token ========= */
        [HttpPost("Autenticar")]
        //public async Task<IActionResult> Login(Usuario aUsuario)
        public IActionResult Login(Autorizacion aUsuario)
        {

            if (aUsuario is null)
                return BadRequest(new { message = "Usuario no enviado" });

            if (aUsuario.UsuarioNombre != "Admin")
                return BadRequest(new { message = "Credenciales Invalidas - " + aUsuario.UsuarioNombre });

            string jwtToken = GenerarToken(aUsuario);
            return Ok(new { token = jwtToken });

        }


        private string GenerarToken(Autorizacion aUsuario)
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

        /*
         
        [Authorize]
        [HttpGet]
        //[Route("GetAllAuthor")]
        public List<Venta> GetVentas()
        {
            return _context.DbSetVenta.ToList();
        }

        [Authorize]
        [HttpGet(template: "{id}")]
        public Venta GetVenta(int id)
        {
            return _context.DbSetVenta.SingleOrDefault(e => e.Id == id);
        }


        [Authorize]
        [HttpDelete(template: "{id}")]
        public IActionResult Delete(int id)
        {
            var vVenta = _context.DbSetVenta.SingleOrDefault(e => e.Id == id);
            if (vVenta == null)
            {
                return NotFound("Venta con id:" + id + " no Existe");
            }
            _context.DbSetVenta.Remove(vVenta);
            _context.SaveChanges();
            return Ok("Venta con id:" + id + " se eliminó correctamente");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddVenta(Venta aVenta)
        {
            _context.DbSetVenta.Add(aVenta);
            _context.SaveChanges();

            return Created("api/ventas/" + aVenta.Id, aVenta);
        }

        [Authorize]
        [HttpPut(template: "{id}")]
        public IActionResult Update(int id, Venta aVenta)
        {
            var vVenta = _context.DbSetVenta.SingleOrDefault(e => e.Id == id);
            if (vVenta == null)
            {
                return NotFound("Venta con id:" + id + " no Existe");
            }

            vVenta.AssesorComercial = aVenta.AssesorComercial;
            vVenta.Fecha = aVenta.Fecha;
            vVenta.Producto = aVenta.Producto;
            vVenta.Cantidad = aVenta.Cantidad;
            vVenta.Precio = aVenta.Precio;

            _context.Update(vVenta);
            _context.SaveChanges();
            return Ok("Venta con id:" + id + " se actualizó correctamente");
        }


        
        [HttpPost("Autenticar")]
        //public async Task<IActionResult> Login(Usuario aUsuario)
        public IActionResult Login(Autorizacion aUsuario)
        {

            if (aUsuario is null)
                return BadRequest(new { message = "Usuario no enviado" });

            if (aUsuario.UsuarioNombre != "Admin")
                return BadRequest(new { message = "Credenciales Invalidas - " + aUsuario.UsuarioNombre });

            string jwtToken = GenerarToken(aUsuario);
            return Ok(new { token = jwtToken });

        }


        private string GenerarToken(Autorizacion aUsuario)
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
        */
    }
}
