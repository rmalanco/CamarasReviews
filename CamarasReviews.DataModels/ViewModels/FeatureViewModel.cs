using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamarasReviews.Models.ViewModels
{
    public class FeatureViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Caracteristicas del Producto")]        
        public string Description { get; set; }
    }
}
