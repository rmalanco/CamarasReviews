using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamarasReviews.Models;

public class ReviewModel
{
    [Key] // esto es para que sea la llave primaria que a ser un Guid
    public Guid ReviewId { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Display(Name = "Titulo")]
    public string Title { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Display(Name = "Descripción Corta")]
    public string ShortDescription { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [MaxLength(5000, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Display(Name = "Descripción Completa")]
    public string LongDescription { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [Display(Name = "Pros")]
    public string Pros { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [Display(Name = "Contras")]
    public string Cons { get; set; }
    [Display(Name = "Fecha de Creación")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [Display(Name = "Fecha de Modificación")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime? ModifiedDate { get; set; }
    [Display(Name = "Fecha de Eliminación")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime? DeletedDate { get; set; }
    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;
    [Required]
    public string AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public ApplicationUser Author { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [ForeignKey("ProductId")]
    public ProductModel Product { get; set; }
    [Display(Name = "Imágenes de la Reseña")]
    public ICollection<ReviewImageModel> ReviewImages { get; set; }
    public ICollection<TagModel> Tags { get; set; }
}
