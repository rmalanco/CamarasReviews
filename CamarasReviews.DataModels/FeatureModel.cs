using System.ComponentModel.DataAnnotations;

namespace CamarasReviews.Models;

public class FeatureModel
{
    [Key]
    public Guid FeatureId { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Display(Name = "Características")]
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<ProductModel> Products { get; set; }
}
