using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PryCafeteria.Models;

namespace PryCafeteria.Controllers
{
    public class MetodosPagosController : Controller
    {
        private readonly BdcafeteriaContext _context;

        public MetodosPagosController(BdcafeteriaContext context)
        {
            _context = context;
        }

        // GET: MetodosPagos
        public async Task<IActionResult> Index()
        {
            return View(await _context.MetodosPagos.ToListAsync());
        }

        // GET: MetodosPagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodosPago = await _context.MetodosPagos
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
            if (metodosPago == null)
            {
                return NotFound();
            }

            return View(metodosPago);
        }

        // GET: MetodosPagos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetodosPagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetodoPagoId,NombreMetodoPago")] MetodosPago metodosPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodosPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metodosPago);
        }

        // GET: MetodosPagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodosPago = await _context.MetodosPagos.FindAsync(id);
            if (metodosPago == null)
            {
                return NotFound();
            }
            return View(metodosPago);
        }

        // POST: MetodosPagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetodoPagoId,NombreMetodoPago")] MetodosPago metodosPago)
        {
            if (id != metodosPago.MetodoPagoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metodosPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodosPagoExists(metodosPago.MetodoPagoId))
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
            return View(metodosPago);
        }

        // GET: MetodosPagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodosPago = await _context.MetodosPagos
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
            if (metodosPago == null)
            {
                return NotFound();
            }

            return View(metodosPago);
        }

        // POST: MetodosPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metodosPago = await _context.MetodosPagos.FindAsync(id);
            if (metodosPago != null)
            {
                _context.MetodosPagos.Remove(metodosPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodosPagoExists(int id)
        {
            return _context.MetodosPagos.Any(e => e.MetodoPagoId == id);
        }
    }
}
