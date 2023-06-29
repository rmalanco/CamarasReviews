using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CamarasReviews.Models.ViewModels
{
    public class ProductViewModel : ProductModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Características del Producto")]
        public string FeatureDescription { get; set; }
        public IEnumerable<SelectListItem>? ListOfCategories { get; set; }
        public IEnumerable<SelectListItem>? ListOfBrands { get; set; }

        // lista de archivos que recibimos desde el formulario para subir al servidor es de tipo IFormFile:
        [Display(Name = "Imagenes del Producto")]
        public List<IFormFile>? Files { get; set; }
    }
}
