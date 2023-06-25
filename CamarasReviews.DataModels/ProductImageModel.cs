using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamarasReviews.Models;

public class ProductImageModel
{
    [Key]
    public Guid ProductImageId { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [Display(Name = "Producto")]
    public Guid ProductId { get; set; }
    [ForeignKey("ProductId")]
    public ProductModel Product { get; set; }
    [DataType(DataType.ImageUrl)]
    [Display(Name = "Imagen de Producto")]
    public string UrlImagen { get; set; }
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
