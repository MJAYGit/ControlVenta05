using ControlVentasBackEnd.Domain;

namespace ControlVentasBackEnd.Infraestructura.Repositories
{
    public interface IAutorizacionRepository
    {
        string GenerarToken(Autorizacion aUsuario);
    }
}
