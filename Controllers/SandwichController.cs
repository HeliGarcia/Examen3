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
    public class SandwichController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;

        public SandwichController(ApplicationDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _hostEnviroment = hostEnviroment;
        }

        // GET: Sandwich
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sandwiches.Include(s => s.TipoPlatillo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sandwich/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sandwiches == null)
            {
                return NotFound();
            }

            var sandwich = await _context.Sandwiches
                .Include(s => s.TipoPlatillo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sandwich == null)
            {
                return NotFound();
            }

            return View(sandwich);
        }

        // GET: Sandwich/Create
        public IActionResult Create()
        {
            ViewData["IdTipoPlatillo"] = new SelectList(_context.TipoPlatillos, "Id", "Nombre");
            return View();
        }

        // POST: Sandwich/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,IdTipoPlatillo,Precio,ImagenSandwich")] Sandwich sandwich)
        {
            string rutaPrincipal = _hostEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if(archivos.Count()>0){
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sandwiches\");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(subidas,nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStream);
                    }
                    sandwich.ImagenSandwich = @"imagenes\sandwiches\" + nombreArchivo + extension;
                }
                _context.Add(sandwich);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        
            ViewData["IdTipoPlatillo"] = new SelectList(_context.TipoPlatillos, "Id", "Nombre", sandwich.IdTipoPlatillo);
            return View(sandwich);
        }

        // GET: Sandwich/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sandwiches == null)
            {
                return NotFound();
            }

            var sandwich = await _context.Sandwiches.FindAsync(id);
            if (sandwich == null)
            {
                return NotFound();
            }
            ViewData["IdTipoPlatillo"] = new SelectList(_context.TipoPlatillos, "Id", "Nombre", sandwich.IdTipoPlatillo);
            return View(sandwich);
        }

        // POST: Sandwich/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,IdTipoPlatillo,Precio")] Sandwich sandwich)
        {
            if (id != sandwich.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sandwich);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SandwichExists(sandwich.Id))
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
            ViewData["IdTipoPlatillo"] = new SelectList(_context.TipoPlatillos, "Id", "Nombre", sandwich.IdTipoPlatillo);
            return View(sandwich);
        }

        // GET: Sandwich/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sandwiches == null)
            {
                return NotFound();
            }

            var sandwich = await _context.Sandwiches
                .Include(s => s.TipoPlatillo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sandwich == null)
            {
                return NotFound();
            }

            return View(sandwich);
        }

        // POST: Sandwich/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sandwiches == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sandwich'  is null.");
            }
            var sandwich = await _context.Sandwiches.FindAsync(id);
            if (sandwich != null)
            {
                _context.Sandwiches.Remove(sandwich);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SandwichExists(int id)
        {
          return (_context.Sandwiches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
