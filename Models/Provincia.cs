using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiPaises.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [ForeignKey("Pais")]
        public int PaisId { get; set; }
    }
}
