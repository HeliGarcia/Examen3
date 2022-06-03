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
    public class HamburguesasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;

        public HamburguesasController(ApplicationDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _hostEnviroment = hostEnviroment;
        }

        // GET: Hamburguesas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hamburguesas.Include(h => h.TipoPlatillo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hamburguesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hamburguesas == null)
            {
                return NotFound();
            }

            var hamburguesa = await _context.Hamburguesas
                .Include(h => h.TipoPlatillo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamburguesa == null)
            {
                return NotFound();
            }

            return View(hamburguesa);
        }

        // GET: Hamburguesas/Create
        public IActionResult Create()
        {
            ViewData["IdTipoPlatillo"] = new SelectList(_context.TipoPlatillos, "Id", "Nombre");
            return View();
        }

        // POST: Hamburguesas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,IdTipoPlatillo,Precio,ImagenHamburguesa")] Hamburguesa hamburguesa)
        {
            
               string rutaPrincipal = _hostEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if(archivos.Count()>0){
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\hamburguesas\");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(subidas,nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStream);
                    }
                    hamburguesa.ImagenHamburguesa = @"imagenes\hamburguesas\" + nombreArchivo + extension;
                }
                _context.Add(hamburguesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["IdTipoPlatillo"] = new SelectList(_context.TipoPlatillos, "Id", "Nombre", hamburguesa.IdTipoPlatillo);
            return View(hamburguesa);
        }

        // GET: Hamburguesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hamburguesas == null)
            {
                return NotFound();
            }

            var hamburguesa = await _context.Hamburguesas.FindAsync(id);
            if (hamburguesa == null)
            {
                return NotFound();
            }
            ViewData["IdTipoPlatillo"] = new SelectList(_context.TipoPlatillos, "Id", "Nombre", hamburguesa.IdTipoPlatillo);
            return View(hamburguesa);
        }

        // POST: Hamburguesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,IdTipoPlatillo,Precio,ImagenHamburguesa")] Hamburguesa hamburguesa)
        {
            if (id != hamburguesa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string rutaPrincipal = _hostEnviroment.WebRootPath;
                    var archivos = HttpContext.Request.Form.Files;
                    if(archivos.Count()>0){
                        Hamburguesa? hamburguesaBD = await _context.Hamburguesas.FindAsync(id);
                        if(hamburguesaBD!=null){
                            if(hamburguesaBD.ImagenHamburguesa!=null){
                                var rutaImagenActual = Path.Combine(rutaPrincipal, hamburguesaBD.ImagenHamburguesa);
                                if(System.IO.File.Exists(rutaImagenActual)){
                                    System.IO.File.Delete(rutaImagenActual);
                                }
                            }
                            _context.Entry(hamburguesaBD).State = EntityState.Detached;
                        }
                        string nombreArchivo = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(rutaPrincipal, @"imagenes\hamburguesas\");
                        var extension = Path.GetExtension(archivos[0].FileName);
                        using (var fileStream = new FileStream(Path.Combine(subidas,nombreArchivo+extension), FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStream);
                        }
                        hamburguesa.ImagenHamburguesa = @"imagenes\hamburguesas\" + nombreArchivo + extension;
                        _context.Entry(hamburguesa).State = EntityState.Modified;
                    }
                    _context.Update(hamburguesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HamburguesaExists(hamburguesa.Id))
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
            ViewData["IdTipoPlatillo"] = new SelectList(_context.TipoPlatillos, "Id", "Nombre", hamburguesa.IdTipoPlatillo);
            return View(hamburguesa);
        }

        // GET: Hamburguesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hamburguesas == null)
            {
                return NotFound();
            }

            var hamburguesa = await _context.Hamburguesas
                .Include(h => h.TipoPlatillo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hamburguesa == null)
            {
                return NotFound();
            }

            return View(hamburguesa);
        }

        // POST: Hamburguesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hamburguesas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Hamburguesas'  is null.");
            }
            var hamburguesa = await _context.Hamburguesas.FindAsync(id);
            if (hamburguesa != null)
            {
                _context.Hamburguesas.Remove(hamburguesa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HamburguesaExists(int id)
        {
          return (_context.Hamburguesas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
