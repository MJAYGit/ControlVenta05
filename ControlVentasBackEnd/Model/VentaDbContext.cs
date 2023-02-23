using Microsoft.EntityFrameworkCore;

namespace ControlVentasBackEnd.Model
{
    public class VentaDbContext : DbContext
    {
        public VentaDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Venta> DbSetVenta { get; set; }
    }
}
