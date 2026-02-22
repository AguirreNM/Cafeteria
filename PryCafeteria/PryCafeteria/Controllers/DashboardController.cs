using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PryCafeteria.Models;

namespace PryCafeteria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly BdcafeteriaContext _context;

        public DashboardController(BdcafeteriaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Total productos
            ViewBag.TotalProductos = await _context.Productos.CountAsync();

            // Total categorías
            ViewBag.TotalCategorias = await _context.Categorias.CountAsync();

            // Total cupones activos
            ViewBag.TotalCupones = await _context.Cupones
                .Where(c => c.Activo == true)
                .CountAsync();

            // Productos con stock bajo (<=5) o agotados
            ViewBag.StockBajo = await _context.ProductosTamanios
                .Where(pt => pt.Stock <= 5)
                .CountAsync();

            // Productos agotados
            ViewBag.Agotados = await _context.ProductosTamanios
                .Where(pt => pt.Stock == 0)
                .CountAsync();

            // Lista de productos con stock bajo para la alerta
            var productosStockBajo = await _context.ProductosTamanios
                .Include(pt => pt.Producto)
                .Include(pt => pt.Tamanio)
                .Where(pt => pt.Stock <= 5)
                .OrderBy(pt => pt.Stock)
                .ToListAsync();

            ViewBag.ProductosStockBajo = productosStockBajo;

            // HU20: pedidos del día
            var hoy = DateTime.Today;
            ViewBag.PedidosHoy = await _context.Pedidos
                .Where(p => p.FechaPedido >= hoy)
                .CountAsync();

            ViewBag.PedidosPendientes = await _context.Pedidos
                .Where(p => p.FechaPedido >= hoy && p.Estado == "Pendiente")
                .CountAsync();

            ViewBag.IngresosHoy = await _context.Pedidos
                .Where(p => p.FechaPedido >= hoy && p.Estado == "Entregado")
                .SumAsync(p => (decimal?)p.Total) ?? 0;

            // Productos más recientes
            ViewBag.UltimosProductos = await _context.Productos
                .Include(p => p.Categoria)
                .OrderByDescending(p => p.ProductoId)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}
