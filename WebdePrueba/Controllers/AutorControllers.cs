using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdePrueba.Context;
using WebdePrueba.Entities;

namespace WebdePrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        public ApplicationDbContext context { get; }
        public AutorController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // Si pongo el Slah me quita el /api/....
        [HttpGet("/Listado")]
        [HttpGet("Listado")]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            Console.WriteLine("Entre");
            return context.Autores.Include(x => x.Libros).ToList();
        }

        [HttpGet("Primer")]
        public ActionResult<Autor> GetPrimerAutor()
        {
            
            return context.Autores.FirstOrDefault();
        }

        // 2 parametros 
        [HttpGet("{id}/{param2?}",Name ="ObtenerAutor")]
        public ActionResult<Autor> Get(int id)
        {
            var autor = context.Autores.Include(x => x.Libros).FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            context.Autores.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor",new { id = autor.Id }, autor);
        }

        [HttpPut("{id}")]
        public ActionResult Put (int id, [FromBody] Autor value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var autor= context.Autores.FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            context.Autores.Remove(autor);
            context.SaveChanges();
            return Ok();
        }


    }
}
