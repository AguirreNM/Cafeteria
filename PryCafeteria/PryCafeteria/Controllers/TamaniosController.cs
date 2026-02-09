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
    public class TamaniosController : Controller
    {
        private readonly BdcafeteriaContext _context;

        public TamaniosController(BdcafeteriaContext context)
        {
            _context = context;
        }

        // GET: Tamanios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tamanios.ToListAsync());
        }

        // GET: Tamanios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tamanio = await _context.Tamanios
                .FirstOrDefaultAsync(m => m.TamanioId == id);
            if (tamanio == null)
            {
                return NotFound();
            }

            return View(tamanio);
        }

        // GET: Tamanios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tamanios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TamanioId,NombreTamanio")] Tamanio tamanio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tamanio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tamanio);
        }

        // GET: Tamanios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tamanio = await _context.Tamanios.FindAsync(id);
            if (tamanio == null)
            {
                return NotFound();
            }
            return View(tamanio);
        }

        // POST: Tamanios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TamanioId,NombreTamanio")] Tamanio tamanio)
        {
            if (id != tamanio.TamanioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tamanio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TamanioExists(tamanio.TamanioId))
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
            return View(tamanio);
        }

        // GET: Tamanios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tamanio = await _context.Tamanios
                .FirstOrDefaultAsync(m => m.TamanioId == id);
            if (tamanio == null)
            {
                return NotFound();
            }

            return View(tamanio);
        }

        // POST: Tamanios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tamanio = await _context.Tamanios.FindAsync(id);
            if (tamanio != null)
            {
                _context.Tamanios.Remove(tamanio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TamanioExists(int id)
        {
            return _context.Tamanios.Any(e => e.TamanioId == id);
        }
    }
}
