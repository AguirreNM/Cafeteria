using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PryCafeteria.Models;

namespace PryCafeteria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductosTamaniosController : Controller
    {
        private readonly BdcafeteriaContext _context;

        public ProductosTamaniosController(BdcafeteriaContext context)
        {
            _context = context;
        }

        // GET: ProductosTamanios
        public async Task<IActionResult> Index()
        {
            var bdcafeteriaContext = _context.ProductosTamanios.Include(p => p.Producto).Include(p => p.Tamanio);
            return View(await bdcafeteriaContext.ToListAsync());
        }

        // GET: ProductosTamanios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosTamanio = await _context.ProductosTamanios
                .Include(p => p.Producto)
                .Include(p => p.Tamanio)
                .FirstOrDefaultAsync(m => m.ProductoTamanioId == id);
            if (productosTamanio == null)
            {
                return NotFound();
            }

            return View(productosTamanio);
        }

        // GET: ProductosTamanios/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto");
            ViewData["TamanioId"] = new SelectList(_context.Tamanios, "TamanioId", "NombreTamanio");
            return View();
        }

        // POST: ProductosTamanios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoTamanioId,ProductoId,TamanioId,Precio,Stock")] ProductosTamanio productosTamanio)
        {
            ModelState.Remove("Producto");
            ModelState.Remove("Tamanio");

            if (ModelState.IsValid)
            {
                // HU08: validar combinación única producto-tamaño
                var existe = await _context.ProductosTamanios.AnyAsync(pt =>
                    pt.ProductoId == productosTamanio.ProductoId &&
                    pt.TamanioId == productosTamanio.TamanioId);
                if (existe)
                {
                    ModelState.AddModelError("", "Ya existe esta combinación de producto y tamaño");
                    ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto", productosTamanio.ProductoId);
                    ViewData["TamanioId"] = new SelectList(_context.Tamanios, "TamanioId", "NombreTamanio", productosTamanio.TamanioId);
                    return View(productosTamanio);
                }

                _context.Add(productosTamanio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto", productosTamanio.ProductoId);
            ViewData["TamanioId"] = new SelectList(_context.Tamanios, "TamanioId", "NombreTamanio", productosTamanio.TamanioId);
            return View(productosTamanio);
        }

        // GET: ProductosTamanios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosTamanio = await _context.ProductosTamanios.FindAsync(id);
            if (productosTamanio == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto", productosTamanio.ProductoId);
            ViewData["TamanioId"] = new SelectList(_context.Tamanios, "TamanioId", "NombreTamanio", productosTamanio.TamanioId);
            return View(productosTamanio);
        }

        // POST: ProductosTamanios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoTamanioId,ProductoId,TamanioId,Precio,Stock")] ProductosTamanio productosTamanio)
        {
            ModelState.Remove("Producto");
            ModelState.Remove("Tamanio");

            if (id != productosTamanio.ProductoTamanioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // HU08: validar combinación única producto-tamaño excluyendo el actual
                var existe = await _context.ProductosTamanios.AnyAsync(pt =>
                    pt.ProductoId == productosTamanio.ProductoId &&
                    pt.TamanioId == productosTamanio.TamanioId &&
                    pt.ProductoTamanioId != productosTamanio.ProductoTamanioId);
                if (existe)
                {
                    ModelState.AddModelError("", "Ya existe esta combinación de producto y tamaño");
                    ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto", productosTamanio.ProductoId);
                    ViewData["TamanioId"] = new SelectList(_context.Tamanios, "TamanioId", "NombreTamanio", productosTamanio.TamanioId);
                    return View(productosTamanio);
                }

                try
                {
                    _context.Update(productosTamanio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosTamanioExists(productosTamanio.ProductoTamanioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "NombreProducto", productosTamanio.ProductoId);
            ViewData["TamanioId"] = new SelectList(_context.Tamanios, "TamanioId", "NombreTamanio", productosTamanio.TamanioId);
            return View(productosTamanio);
        }

        // GET: ProductosTamanios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosTamanio = await _context.ProductosTamanios
                .Include(p => p.Producto)
                .Include(p => p.Tamanio)
                .FirstOrDefaultAsync(m => m.ProductoTamanioId == id);
            if (productosTamanio == null)
            {
                return NotFound();
            }

            return View(productosTamanio);
        }

        // POST: ProductosTamanios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoTamanio = await _context.ProductosTamanios
                .Include(pt => pt.DetallePedidos)
                .FirstOrDefaultAsync(pt => pt.ProductoTamanioId == id);

            if (productoTamanio == null)
                return NotFound();

            // HU08 - E6: bloquear si tiene ventas
            if (productoTamanio.DetallePedidos.Any())
            {
                TempData["Error"] = "No se puede eliminar: este tamaño tiene ventas registradas. Considera marcarlo como Sin stock.";
                return RedirectToAction(nameof(Index));
            }

            _context.ProductosTamanios.Remove(productoTamanio);
            await _context.SaveChangesAsync();
            TempData["Exito"] = "Tamaño eliminado correctamente";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosTamanioExists(int id)
        {
            return _context.ProductosTamanios.Any(e => e.ProductoTamanioId == id);
        }
    }
}
