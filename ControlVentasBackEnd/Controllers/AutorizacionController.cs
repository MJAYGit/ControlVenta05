using Microsoft.AspNetCore.Mvc;

using ControlVentasBackEnd.Domain;
using ControlVentasBackEnd.Infraestructura.Repositories;
using Microsoft.AspNetCore.Cors;


namespace ControlVentasBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class AutorizacionController : ControllerBase
    {
        private readonly IAutorizacionRepository _AutorizacionRepository;

        public AutorizacionController(IAutorizacionRepository aAutorizacionRepository)
        { 
            _AutorizacionRepository = aAutorizacionRepository;
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

            string jwtToken = _AutorizacionRepository.GenerarToken(aUsuario);
            aUsuario.Token = jwtToken;
            //return Ok(new { token = jwtToken });
            return Ok(aUsuario);

        }
    }
}
