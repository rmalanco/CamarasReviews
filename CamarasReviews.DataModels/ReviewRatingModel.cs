using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamarasReviews.Models;

public class ReviewRatingModel
{
    [Key]
    public Guid ReviewRatingId { get; set; }
    [Required]
    public Guid ReviewId { get; set; }
    [ForeignKey("ReviewId")]
    public ReviewModel Review { get; set; }
    [Required]
    public Guid RatingId { get; set; }
    [ForeignKey("RatingId")]
    public RatingModel Rating { get; set; }
}
