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
        public string ProductId { get; set; }
        public ProductModel Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
        [Display(Name = "Descripción de características")]
        public string FeatureDescription { get; set; }
    }
}
