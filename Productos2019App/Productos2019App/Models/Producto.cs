using System.ComponentModel.DataAnnotations;

namespace Productos2019App.Models
{
    // Representa un producto en la aplicación.
    // Incluye el identificador, el nombre, la categoría a la que pertenece y 
    // la colección de ventas asociadas.
    public class Producto
    {
        // Identificador único del producto.
        [Key]
        public int CodigoProducto { get; set; }

        // Nombre del producto.
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        // Identificador de la categoría a la que pertenece el producto.
        public int CodigoCategoria { get; set; }

        // Categoría asociada al producto.
        public Categoria Categoria { get; set; }

        // Colección de ventas en las que aparece este producto.
        public ICollection<Venta> Venta { get; set; } = new List<Venta>();
    }
}
