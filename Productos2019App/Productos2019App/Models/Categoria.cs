using System.ComponentModel.DataAnnotations;

namespace Productos2019App.Models
{
    // Representa una categoría de productos en la aplicación.
    // Incluye el identificador, el nombre y la colección de productos asociados.
    public class Categoria
    {
        // Identificador único de la categoría.
        [Key]
        public int CodigoCategoria { get; set; }

        // Nombre de la categoría.
        [Required] 
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        // Colección de productos que pertenecen a esta categoría.
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
