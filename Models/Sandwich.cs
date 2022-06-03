using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen3.Models
{
    public class Sandwich
    {
        [Key]
        public int Id {get; set;}
        [Required]
        [Display(Name = "Nombre del Sandwich")]
        public string Nombre { get; set; }
        [Display(Name = "Tipo de platillo")]
        public int IdTipoPlatillo { get; set; }
        [ForeignKey("IdTipoPlatillo")]
        public TipoPlatillo TipoPlatillo { get; set; }
        public double Precio { get; set; }
        public string? ImagenSandwich { get; set; }
    }
}