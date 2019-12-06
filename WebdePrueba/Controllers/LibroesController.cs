using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebdePrueba.Context;
using WebdePrueba.Entities;

namespace WebdePrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibroesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Libroes
        [HttpGet]
        public  ActionResult<IEnumerable<Libro>> Get()
        {
            return _context.Libros.Include(x => x.Autor).ToList();
        }

        // GET: api/Libroes/5
        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = _context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        // PUT: api/Libroes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

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

            return NoContent();
        }




        // POST: api/Libroes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            _context.Libros.Add(libro);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = libro.Id }, libro);
        }

        // DELETE: api/Libroes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Libro>> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return libro;
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}
