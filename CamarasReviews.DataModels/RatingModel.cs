using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamarasReviews.Models
{
    public class RatingModel
    {
        [Key]
        public Guid RatingId { get; set; }
        public int RatingValue { get; set; }

        // relacion de votos ya que un usuario puede tener muchos votos
        public ApplicationUser User { get; set; }

        // relacion de votos ya que un review puede tener muchos votos
        public ReviewModel Review { get; set; }
    }
}