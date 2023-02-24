using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlVentasBackEnd.Model;

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
