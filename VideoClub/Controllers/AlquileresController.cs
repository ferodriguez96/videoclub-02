using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoClub.DataBase;
using VideoClub.Models;

namespace VideoClub.Controllers
{
    public class AlquileresController : Controller
    {
        private readonly VideoClubDbContext _context;

        public AlquileresController(VideoClubDbContext context)
        {
            _context = context;
        }

        // GET: Alquileres
        public async Task<IActionResult> Index()
        {
            var videoClubDbContext = _context.Alquileres.Include(a => a.Cliente).Include(a => a.Devolucion).Include(a => a.Pelicula);
            return View(await videoClubDbContext.ToListAsync());
        }

        // GET: Alquileres/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquileres
                .Include(a => a.Cliente)
                .Include(a => a.Devolucion)
                .Include(a => a.Pelicula)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // GET: Alquileres/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido");
            ViewData["DevolucionId"] = new SelectList(_context.Devoluciones, "Id", "Id");
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo");
            return View();
        }

        // POST: Alquileres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PeliculaId,ClienteId,FechaAlta,FechaVencimiento,PrecioOriginal,DevolucionId")] Alquiler alquiler)
        {
            if (ModelState.IsValid)
            {
                alquiler.Id = Guid.NewGuid();
                _context.Add(alquiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido", alquiler.ClienteId);
            ViewData["DevolucionId"] = new SelectList(_context.Devoluciones, "Id", "Id", alquiler.DevolucionId);
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Id", alquiler.PeliculaId);
            return View(alquiler);
        }

        // GET: Alquileres/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquileres.FindAsync(id);
            if (alquiler == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido", alquiler.ClienteId);
            ViewData["DevolucionId"] = new SelectList(_context.Devoluciones, "Id", "Id", alquiler.DevolucionId);
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Id", alquiler.PeliculaId);
            return View(alquiler);
        }

        // POST: Alquileres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PeliculaId,ClienteId,FechaAlta,FechaVencimiento,PrecioOriginal,DevolucionId")] Alquiler alquiler)
        {
            if (id != alquiler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alquiler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlquilerExists(alquiler.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellido", alquiler.ClienteId);
            ViewData["DevolucionId"] = new SelectList(_context.Devoluciones, "Id", "Id", alquiler.DevolucionId);
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Id", alquiler.PeliculaId);
            return View(alquiler);
        }

        // GET: Alquileres/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquileres
                .Include(a => a.Cliente)
                .Include(a => a.Devolucion)
                .Include(a => a.Pelicula)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // POST: Alquileres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var alquiler = await _context.Alquileres.FindAsync(id);
            _context.Alquileres.Remove(alquiler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilerExists(Guid id)
        {
            return _context.Alquileres.Any(e => e.Id == id);
        }
    }
}
