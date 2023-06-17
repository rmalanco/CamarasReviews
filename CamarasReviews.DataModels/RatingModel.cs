using System.ComponentModel.DataAnnotations;

namespace CamarasReviews.Models
{
    public class RatingModel
    {
        [Key]
        public Guid RatingId { get; set; }
        [Required]
        public string RatingName { get; set; }
        [Required]
        public int RatingValue { get; set; }
    }
}