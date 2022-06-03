using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen3.Models
{
    public class Entrada
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre de entrada")]
        public string Nombre { get; set; }
        [Display(Name = "Tipo de platillo")]
        public int IdTipoPlatillo { get; set; }
        [ForeignKey("IdTipoPlatillo")]
        public TipoPlatillo TipoPlatillo { get; set; }
        public double Precio { get; set; }
        
        public string? ImagenEntrada { get; set; }
    }
}