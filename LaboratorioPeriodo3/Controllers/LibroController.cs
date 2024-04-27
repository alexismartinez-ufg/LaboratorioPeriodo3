using LaboratorioPeriodo3.DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LaboratorioPeriodo3.Controllers
{
    public class LibroController : Controller
    {
        private readonly LaboratorioPeriodo3Context _context;

        public LibroController(LaboratorioPeriodo3Context context)
        {
            _context = context;
        }

        // GET: Libro/Index
        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros
                                       .Include(l => l.LibrosAutores)
                                       .ThenInclude(la => la.Autor)
                                       .ToListAsync();
            return View(libros);
        }

        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var libro = await _context.Libros
                                      .Include(l => l.LibrosAutores)
                                      .ThenInclude(la => la.Autor)
                                      .FirstOrDefaultAsync(l => l.Codigo == id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Libros.Add(libro);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Libro/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var libro = _context.Libros.Include(l => l.LibrosAutores)
                                      .ThenInclude(la => la.Autor).FirstOrDefault(x=>x.Codigo==id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro)
        {
            if (id != libro.Codigo)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(libro).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(id))
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
            return View(libro);
        }

        // GET: Libro/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var libro = await _context.Libros
                                      .Include(l => l.LibrosAutores)
                                      .ThenInclude(la => la.Autor)
                                      .FirstOrDefaultAsync(l => l.Codigo == id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros
                                      .Include(l => l.LibrosAutores)
                                      .FirstOrDefaultAsync(l => l.Codigo == id);

            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Autores(int id)
        {
            var libro = await _context.Libros
                                      .Include(l => l.LibrosAutores)
                                      .ThenInclude(la => la.Autor)
                                      .FirstOrDefaultAsync(l => l.Codigo == id);

            if (libro == null)
            {
                return NotFound();
            }

            // Obtener todos los autores, excluyendo los ya asociados con el libro
            var autoresAsociados = libro.LibrosAutores.Select(la => la.AutorId).ToList();
            var autores = await _context.Autores
                                        .Where(a => !autoresAsociados.Contains(a.AutorId))
                                        .ToListAsync();

            ViewBag.Autores = new SelectList(autores, "AutorId", "NombreCompleto");

            return View(libro);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Autores(int Codigo, int AutorId)
        {
            var libroAutore = new LibrosAutore
            {
                CodigoLibro = Codigo,
                AutorId = AutorId
            };

            _context.LibrosAutores.Add(libroAutore);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EliminarAutorLibro(int Codigo, int AutorId)
        {
            var relacion = await _context.LibrosAutores
                                         .FirstOrDefaultAsync(la => la.CodigoLibro == Codigo && la.AutorId == AutorId);

            if (relacion == null)
            {
                return NotFound();
            }

            _context.LibrosAutores.Remove(relacion);
            await _context.SaveChangesAsync();

            return RedirectToAction("Autores", new { id = Codigo });
        }


        private bool LibroExists(int id)
        {
            return _context.Libros.Any(l => l.Codigo == id);
        }
    }
}
