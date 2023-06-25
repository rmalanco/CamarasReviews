using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamarasReviews.Models;

public class ProductModel
{
    [Key]
    public Guid ProductId { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    [Display(Name = "Nombre del Producto")]
    public string Name { get; set; }
    // [Required(ErrorMessage = "El campo {0} es requerido.")]
    // [MaxLength(5000, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
    // [Display(Name = "Descripción del Producto")]
    // public string Description { get; set; }
    // [Required(ErrorMessage = "El campo {0} es requerido.")]
    // [Display(Name = "SKU")]
    // public string SKU { get; set; }
    // [Required(ErrorMessage = "El campo {0} es requerido.")]
    // [Display(Name = "Precio")]
    // public double Price { get; set; }
    // [Display(Name = "Categoría")]
    // public Guid CategoryId { get; set; }
    // [ForeignKey("CategoryId")]
    // public CategoryModel Category { get; set; }
    // [Display(Name = "Marca")]
    // public Guid BrandId { get; set; }
    // [ForeignKey("BrandId")]
    // public BrandModel Brand { get; set; }
    // [Display(Name = "Fecha de Creación")]
    // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
    // public DateTime CreatedDate { get; set; } = DateTime.Now;
    // [Display(Name = "Fecha de Modificación")]
    // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
    // public DateTime? ModifiedDate { get; set; }
    // [Display(Name = "Fecha de Eliminación")]
    // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
    // public DateTime? DeletedDate { get; set; }
    // [Display(Name = "Activo")]
    // public bool IsActive { get; set; } = true;
}
