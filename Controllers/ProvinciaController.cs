using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiPaises.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApiPaises.Controllers
{
    [Produces("application/json")]
    [Route("api/pais/{PaisId}/provincia")]
    public class ProvinciaController : Controller
    {
        public readonly ApplicationDbContext context;

        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Provincia> GetAll(int PaisId)
        {
            return context.Provincias.Where(x => x.PaisId == PaisId).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerProvinciaById")]
        public IActionResult GetById(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(x => x.PaisId == id);
            if (provincia == null)
            {
                return NotFound();
            }

            return new ObjectResult(provincia);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Provincia provincia, int id)
        {
            provincia.PaisId = id;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Provincias.Add(provincia);
            context.SaveChanges();
            return CreatedAtRoute("ObtenerProvincinaById", new { id = provincia.Id }, provincia);

        }

        [HttpPut("${id}")]
        public IActionResult Put([FromBody] Provincia provincia, int id){
            if(provincia.Id == id){
                return BadRequest();
            }

            context.Entry(provincia).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            Provincia provincia = context.Provincias.FirstOrDefault( x => x.Id == id);
            if(provincia == null){
                return NotFound();
            }
            context.Provincias.Remove(provincia);
            context.SaveChanges();
            return Ok(provincia);
        }
    }

}
