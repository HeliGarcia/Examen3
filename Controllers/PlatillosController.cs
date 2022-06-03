using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen3.Data;
using Examen3.Models;

namespace Examen3.Controllers
{
    public class PlatillosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlatillosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Platillos
        public async Task<IActionResult> Index()
        {
              return _context.TipoPlatillos != null ? 
                          View(await _context.TipoPlatillos.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TipoPlatillos'  is null.");
        }

        // GET: Platillos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoPlatillos == null)
            {
                return NotFound();
            }

            var tipoPlatillo = await _context.TipoPlatillos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPlatillo == null)
            {
                return NotFound();
            }

            return View(tipoPlatillo);
        }

        // GET: Platillos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platillos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoPlatillo tipoPlatillo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoPlatillo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPlatillo);
        }

        // GET: Platillos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoPlatillos == null)
            {
                return NotFound();
            }

            var tipoPlatillo = await _context.TipoPlatillos.FindAsync(id);
            if (tipoPlatillo == null)
            {
                return NotFound();
            }
            return View(tipoPlatillo);
        }

        // POST: Platillos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] TipoPlatillo tipoPlatillo)
        {
            if (id != tipoPlatillo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPlatillo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPlatilloExists(tipoPlatillo.Id))
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
            return View(tipoPlatillo);
        }

        // GET: Platillos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoPlatillos == null)
            {
                return NotFound();
            }

            var tipoPlatillo = await _context.TipoPlatillos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPlatillo == null)
            {
                return NotFound();
            }

            return View(tipoPlatillo);
        }

        // POST: Platillos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoPlatillos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TipoPlatillos'  is null.");
            }
            var tipoPlatillo = await _context.TipoPlatillos.FindAsync(id);
            if (tipoPlatillo != null)
            {
                _context.TipoPlatillos.Remove(tipoPlatillo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPlatilloExists(int id)
        {
          return (_context.TipoPlatillos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
