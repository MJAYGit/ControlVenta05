using System.Collections.Generic;
using System.Threading.Tasks;
using ControlVentasBackEnd.Domain;

namespace ControlVentasBackEnd.Infraestructura.Repositories
{
    public interface IVentaRepository
    {
        Task<List<Venta>> GetVentas();
        Task<Venta> GetVenta(int id);
        Task  Delete(int id);
        Task AddVenta(Venta aVenta);
        Task Update(int id, Venta aVenta);

    }
}
