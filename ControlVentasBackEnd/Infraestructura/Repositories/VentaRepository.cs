using ControlVentasBackEnd.Domain;
using ControlVentasBackEnd.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ControlVentasBackEnd.Infraestructura.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly VentaDbContext _context;

        public VentaRepository(VentaDbContext context)
        {
            _context = context;
            //_config = config;
        }

        public async Task AddVenta(Venta aVenta)
        {
            _context.DbSetVenta.Add(aVenta);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var vVenta = _context.DbSetVenta.SingleOrDefault(e => e.Id == id);
            if (vVenta == null)
            {
                //return "Venta con id:" + id + " no Existe";
                return;
            }
            _context.DbSetVenta.Remove(vVenta);            
            await _context.SaveChangesAsync();
        }

        public async Task<Venta> GetVenta(int id)
        {
            return await _context.DbSetVenta.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Venta>> GetVentas()
        {
            return await _context.DbSetVenta.ToListAsync();
        }

        public async Task Update(int id, Venta aVenta)
        {
            var vVenta = _context.DbSetVenta.SingleOrDefault(e => e.Id == id);
            if (vVenta == null)
            {
                //return NotFound("Venta con id:" + id + " no Existe");
                return;
            }

            vVenta.AssesorComercial = aVenta.AssesorComercial;
            vVenta.Fecha = aVenta.Fecha;
            vVenta.Producto = aVenta.Producto;
            vVenta.Cantidad = aVenta.Cantidad;
            vVenta.Precio = aVenta.Precio;

            _context.Update(vVenta);
            await _context.SaveChangesAsync();
        }
    }
}
