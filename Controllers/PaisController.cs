using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiPaises.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiPaises.Controllers
{
    [Produces("application/json")]
    [Route("api/pais")]
    public class PaisController : Controller
    {
        private readonly ApplicationDbContext context;

        public PaisController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "ObtenerPaises")]
        public IEnumerable<Pais> Get()
        {
            var valores = new List<int> { 1, 2, 3, 4, 5, 6 };
            var suma = valores.Sum();
            var pares = valores.Where(x => x % 2 == 0).ToList();

            return this.context.Paises.ToList(); //Devuelve un arreglo
        }

        [HttpGet("{id}", Name = "PaisCreado")]
        public IActionResult GetById(int id)
        {
            var pais = context.Paises.Include(x => x.Provincias).FirstOrDefault(x => x.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            return Ok(pais);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Paises.Add(pais);
                context.SaveChanges();

                //con esto llamamos el metodo que recibe un id y prsentamos la informacion en pantalla
                return new CreatedAtRouteResult("PaisCreado", new { id = pais.Id }, pais);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Pais pais, int id)
        {
            if (pais.Id != id)
            {
                return BadRequest();
            }

            context.Entry(pais).State = EntityState.Modified;
            context.SaveChanges();
            return new CreatedAtRouteResult("paisCreado", new { id = pais.Id }, pais);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pais = context.Paises.FirstOrDefault(x => x.Id == id);
            if (pais == null)
            {
                return NotFound();
            }

            context.Paises.Remove(pais);
            context.SaveChanges();
            return Ok();
        }
    }
}