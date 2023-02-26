using Microsoft.EntityFrameworkCore;
using ControlVentasBackEnd.Domain;

namespace ControlVentasBackEnd.Infraestructura.Data
{
    public class VentaDbContext : DbContext
    {
        public VentaDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Venta> DbSetVenta { get; set; }
    }
}
