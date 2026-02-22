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
    public class ProductosController : Controller
    {
        private readonly BdcafeteriaContext _context;

        public ProductosController(BdcafeteriaContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var bdcafeteriaContext = _context.Productos.Include(p => p.Categoria);
            return View(await bdcafeteriaContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,NombreProducto,Descripcion,CategoriaId,Imagen,Disponible")] Producto producto)
        {
            ModelState.Remove("Categoria");

            if (ModelState.IsValid)
            {
                // HU04 - E8: validar nombre duplicado
                var existe = await _context.Productos.AnyAsync(p =>
                    p.NombreProducto.ToLower() == producto.NombreProducto.ToLower());
                if (existe)
                {
                    ModelState.AddModelError("NombreProducto", "Ya existe un producto con este nombre");
                    ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria", producto.CategoriaId);
                    return View(producto);
                }

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria", producto.CategoriaId);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,NombreProducto,Descripcion,CategoriaId,Imagen,Disponible")] Producto producto)
        {
            ModelState.Remove("Categoria");

            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // HU04 - E8: validar nombre duplicado excluyendo el actual
                var existe = await _context.Productos.AnyAsync(p =>
                    p.NombreProducto.ToLower() == producto.NombreProducto.ToLower() &&
                    p.ProductoId != producto.ProductoId);
                if (existe)
                {
                    ModelState.AddModelError("NombreProducto", "Ya existe un producto con este nombre");
                    ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria", producto.CategoriaId);
                    return View(producto);
                }

                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.ProductosTamanios)
                    .ThenInclude(pt => pt.DetallePedidos)
                .FirstOrDefaultAsync(p => p.ProductoId == id);

            if (producto == null)
                return NotFound();

            // HU04 - E6: bloquear si tiene ventas registradas
            var tieneVentas = producto.ProductosTamanios
                .Any(pt => pt.DetallePedidos.Any());

            if (tieneVentas)
            {
                TempData["Error"] = "No se puede eliminar: el producto tiene ventas registradas. Considera marcarlo como No disponible.";
                return RedirectToAction(nameof(Index));
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            TempData["Exito"] = "Producto eliminado correctamente";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}
