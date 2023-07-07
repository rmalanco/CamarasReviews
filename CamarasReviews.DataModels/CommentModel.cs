using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamarasReviews.Models;

public class CommentModel
{
    [Key]
    public Guid ReviewCommentId { get; set; }
    public Guid ReviewId { get; set; }
    public ReviewModel? Review { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [MaxLength(5000, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Display(Name = "Comentario")]
    public string Comment { get; set; }
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
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public string UserName { get; set; }
}
