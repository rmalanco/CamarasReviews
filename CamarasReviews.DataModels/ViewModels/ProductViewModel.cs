using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductModel Product { get; set; }
        public FeatureModel Feature { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
        public IEnumerable<SelectListItem> ListaMarcas { get; set; }
    }
}
