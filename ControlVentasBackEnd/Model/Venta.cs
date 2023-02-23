
namespace ControlVentasBackEnd.Model
{
    public class Venta
    {
        public int Id { get; set; }
        public string AssesorComercial { get; set; }
        public string Fecha { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

    }
}
