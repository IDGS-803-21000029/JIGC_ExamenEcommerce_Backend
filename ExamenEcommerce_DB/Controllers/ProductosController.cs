using ExamenEcommerce_DB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamenEcommerce_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        // Variable de Contexto de Base de Datos
        private readonly ExamenEcommerceDbContext _baseDatos;

        public ProductosController (ExamenEcommerceDbContext baseDatos)
        {
            this._baseDatos = baseDatos;
        }

        // GET: Listado de Productos (por categoria)
        [HttpGet]
        [Route("ObtenerProductos")]
        public async Task<IActionResult> ObtenerProductos(string categoria = null)
        {
            List<Producto> listaProductos;

            if (string.IsNullOrEmpty(categoria))
            {
                listaProductos = await _baseDatos.Productos.ToListAsync();
            }
            else
            {
                listaProductos = await _baseDatos.Productos
                    .Where(p => p.Categoria == categoria)
                    .ToListAsync();
            }

            return Ok(listaProductos);
        }


        // GET: Listado productos dinamico
        [HttpGet]
        [Route("RandomProductos")]
        public async Task<IActionResult> ObtenerProductosAleatorios()
        {
            var totalProductos = await _baseDatos.Productos.CountAsync();

            if (totalProductos < 3)
            {
                return BadRequest("No hay suficientes productos");
            }

            List<Producto> productosAleatorios = new List<Producto>();

            var random = new Random();
            var indicesElegidos = new HashSet<int>();

            while (indicesElegidos.Count < 3)
            {
                var indiceAleatorio = random.Next(0, totalProductos);

                if (!indicesElegidos.Contains(indiceAleatorio))
                {
                    indicesElegidos.Add(indiceAleatorio);

                    var productoAleatorio = await _baseDatos.Productos
                        .Skip(indiceAleatorio)
                        .FirstOrDefaultAsync();

                    if (productoAleatorio != null)
                    {
                        productosAleatorios.Add(productoAleatorio);
                    }
                }
            }

            return Ok(productosAleatorios);
        }

        // POST: Agregar Producto
        [HttpPost]
        [Route("AgregarProducto")]
        public async Task<IActionResult> AgregarProducto([FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest("El producto enviado es nulo.");
            }

            try
            {
                // Agregar el producto al contexto y guardar cambios en la base de datos
                await _baseDatos.Productos.AddAsync(producto);
                await _baseDatos.SaveChangesAsync();

                return Ok("Producto agregado correctamente.");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error al agregar el producto: {ex.Message}");
            }
        }
    }
}
