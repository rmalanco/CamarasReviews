using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CamarasReviews.Models.ViewModels
{
    public class ProductViewModel : ProductModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Características del Producto")]
        public string FeatureDescription { get; set; }
    }
}
