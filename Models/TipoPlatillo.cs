using System.ComponentModel.DataAnnotations;

namespace Examen3.Models
{
    public class TipoPlatillo
    {
        [Key]
        public int Id { get; set;}
        [Required]
        [Display(Name = "Tipo de platillo")]
        public string Nombre { get; set; }
    }
}