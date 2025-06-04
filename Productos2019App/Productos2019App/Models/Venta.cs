using System.ComponentModel.DataAnnotations;

namespace Productos2019App.Models
{
    // Representa una venta realizada en la aplicación.
    // Incluye el identificador, la fecha, el producto vendido y su relación.

    public class Venta
    {
        // Identificador único de la venta.
        [Key]
        public int CodigoVenta { get; set; }

        // Fecha en que se realizó la venta.
        [Required]
        public DateTime Fecha { get; set; }

        // Identificador del producto vendido.
        public int CodigoProducto { get; set; }

        // Producto asociado a la venta.
        public Producto Producto { get; set; }
    }
}
