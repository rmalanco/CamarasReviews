using System.ComponentModel.DataAnnotations;
using CamarasReviews.Models;

namespace CamarasReviews.Models;

public class BrandModel
{
    [Key]
    public Guid BrandId { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [Display(Name = "Nombre de la marca")]
    public string Name { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [Display(Name = "Descripción")]
    public string Description { get; set; }
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
}
