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
    public class PeliculasController : Controller
    {
        private readonly VideoClubDbContext _context;

        public PeliculasController(VideoClubDbContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index()
        {
            var peliculas = await _context
                .Peliculas
                .Include(p => p.Categoria)
                .Include(p => p.PeliculaGeneros).ThenInclude(pg => pg.Genero)
                .ToListAsync();

            return View(peliculas);
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Categoria)
                .Include(p => p.PeliculaGeneros).ThenInclude(pg => pg.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Descripcion");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Duracion,Stock,AnioLanzamiento,CategoriaId,generos")] Pelicula pelicula, List<Guid> generos)
        {
            if (ModelState.IsValid)
            {
                pelicula.Id = Guid.NewGuid();
                _context.Add(pelicula);

                foreach(var genero in generos)
                {
                    _context.Add(new PeliculaGenero() { Id = Guid.NewGuid(), PeliculaId = pelicula.Id, GeneroId = genero });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Descripcion", generos);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", pelicula.CategoriaId);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context
                .Peliculas
                .Include(p => p.PeliculaGeneros)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Descripcion", pelicula.PeliculaGeneros.Select(p => p.GeneroId));
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", pelicula.CategoriaId);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Titulo,Duracion,Stock,AnioLanzamiento,CategoriaId,generos")] Pelicula pelicula, List<Guid> generos)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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

            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Descripcion", generos);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", pelicula.CategoriaId);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(Guid id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}
