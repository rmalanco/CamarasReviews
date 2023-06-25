using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CamarasReviews.Models.ViewModels
{
    public class ProductViewModel
    {
        public FeatureModel Feature { get; set; }
        public ProductImageModel ProductImage { get; set; }
        public IEnumerable<SelectListItem> ListaMarcas { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
