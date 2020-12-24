using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApiPaises.Models
{
    public class Pais
    {
        public Pais()
        {
            Provincias = new List<Provincia>();
        }
        
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "No puede contener mas de 30 caracteres")]
        public string Nombre { get; set; }
        public List<Provincia> Provincias { get; set; }
    }
}
