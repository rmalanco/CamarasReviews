using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamarasReviews.Models;

public class FeatureModel
{
    [Key]
    public Guid FeatureId { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Display(Name = "Características")]
    public string Description { get; set; }
    [Display(Name = "Producto")]
    public Guid ProductId { get; set; }
    public ProductModel? Product { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsActive { get; set; } = true;
}
