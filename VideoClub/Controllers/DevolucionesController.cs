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
    public class DevolucionesController : Controller
    {
        private readonly VideoClubDbContext _context;

        public DevolucionesController(VideoClubDbContext context)
        {
            _context = context;
        }

        // GET: Devoluciones
        public async Task<IActionResult> Index()
        {
            var videoClubDbContext = _context.Devoluciones.Include(d => d.Alquiler);
            return View(await videoClubDbContext.ToListAsync());
        }

        // GET: Devoluciones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devolucion = await _context.Devoluciones
                .Include(d => d.Alquiler)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (devolucion == null)
            {
                return NotFound();
            }

            return View(devolucion);
        }

        // GET: Devoluciones/Create
        public IActionResult Create()
        {
            ViewData["AlquilerId"] = new SelectList(_context.Alquileres, "Id", "Id");
            return View();
        }

        // POST: Devoluciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaDevolucion,PrecioFinal,AlquilerId")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                devolucion.Id = Guid.NewGuid();
                _context.Add(devolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlquilerId"] = new SelectList(_context.Alquileres, "Id", "Id", devolucion.AlquilerId);
            return View(devolucion);
        }

        // GET: Devoluciones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devolucion = await _context.Devoluciones.FindAsync(id);
            if (devolucion == null)
            {
                return NotFound();
            }
            ViewData["AlquilerId"] = new SelectList(_context.Alquileres, "Id", "Id", devolucion.AlquilerId);
            return View(devolucion);
        }

        // POST: Devoluciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FechaDevolucion,PrecioFinal,AlquilerId")] Devolucion devolucion)
        {
            if (id != devolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(devolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevolucionExists(devolucion.Id))
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
            ViewData["AlquilerId"] = new SelectList(_context.Alquileres, "Id", "Id", devolucion.AlquilerId);
            return View(devolucion);
        }

        // GET: Devoluciones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devolucion = await _context.Devoluciones
                .Include(d => d.Alquiler)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (devolucion == null)
            {
                return NotFound();
            }

            return View(devolucion);
        }

        // POST: Devoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var devolucion = await _context.Devoluciones.FindAsync(id);
            _context.Devoluciones.Remove(devolucion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DevolucionExists(Guid id)
        {
            return _context.Devoluciones.Any(e => e.Id == id);
        }
    }
}
