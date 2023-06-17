using System.ComponentModel.DataAnnotations;

namespace CamarasReviews.Models
{
    public class RatingModel
    {
        [Key]
        public Guid RatingId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Calificacion")]        
        public string RatingName { get; set; }
        [Required]
        [Display(Name = "Valor")]
        public int RatingValue { get; set; }
    }
}