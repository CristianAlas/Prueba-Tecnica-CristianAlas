using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Productos2019App.Models;
using Microsoft.EntityFrameworkCore;


namespace Productos2019App.Controllers
{
    // Controlador responsable de gestionar las operaciones relacionadas con los productos y sus ventas.
    // Permite filtrar ventas por categoría y mostrar las categorías disponibles con ventas en 2019.
    public class ProductosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductosController> _logger;

        // Constructor del controlador. Inicializa el contexto de base de datos y el logger.
        // Contexto de base de datos de la aplicación.
        // Logger para registrar errores y eventos.
        public ProductosController(AppDbContext context, ILogger<ProductosController> logger = null)
        {
            _context = context;
            _logger = logger;
        }

        // Muestra la página principal de productos, permitiendo filtrar ventas por categoría.
        // Si no se selecciona una categoría, no se muestran ventas.
        public async Task<IActionResult> Index(int? categoriaId)
        {
            try
            {
                // Obtiene las categorías con ventas en 2019
                var categorias = await _context.Categorias
                    .Where(c => c.Productos.Any(p => p.Venta.Any(v => v.Fecha.Year == 2019)))
                    .OrderBy(c => c.Nombre) 
                    .ToListAsync();

                ViewBag.Categorias = new SelectList(categorias, "CodigoCategoria", "Nombre");

                // Consulta las ventas del año 2019
                var ventasQuery = _context.Venta
                    .Include(v => v.Producto)
                    .ThenInclude(p => p.Categoria)
                    .Where(v => v.Fecha.Year == 2019);

                if (categoriaId.HasValue)
                {
                    // Filtra las ventas por la categoría seleccionada
                    ventasQuery = ventasQuery.Where(v => v.Producto.CodigoCategoria == categoriaId.Value);
                }
                else
                {
                    // Si no hay categoría seleccionada, no se muestran ventas
                    ViewBag.Ventas = new List<Venta>();
                    return View();
                }

                // Obtiene la lista de ventas filtradas
                var ventasFiltradas = await ventasQuery
                    .OrderByDescending(v => v.Fecha) 
                    .ToListAsync(); 

                ViewBag.Ventas = ventasFiltradas;

                return View();
            }
            catch (Exception ex)
            {
                // Manejo de errores: registra el error y muestra un mensaje al usuario
                _logger?.LogError(ex, "Error al obtener las ventas");

                ViewBag.Ventas = new List<Venta>();
                ViewBag.Categorias = new SelectList(new List<Categoria>(), "CodigoCategoria", "Nombre");
                ViewBag.Error = "Ocurrió un error al cargar los datos.";

                return View();
            }
        }
    }
}

