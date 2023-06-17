using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamarasReviews.Models;

public class ReviewCommentModel
{
    [Key]
    public Guid ReviewCommentId { get; set; }
    [Required]
    public Guid ReviewId { get; set; }
    [ForeignKey("ReviewId")]
    public ReviewModel Review { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [MaxLength(5000, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Display(Name = "Comentario")]
    public string Comment { get; set; }
    [Display(Name = "Fecha de Creacion")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [Display(Name = "Fecha de Modificacion")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime? ModifiedDate { get; set; }
    [Display(Name = "Fecha de Eliminacion")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
    public DateTime? DeletedDate { get; set; }
    [Display(Name = "Activo")]
    public bool IsActive { get; set; }
}
