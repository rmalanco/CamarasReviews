using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Models.ViewModels
{
    public class ReviewViewModel
    {
        public ProductModel Product { get; set; }
        public FeatureModel Feature { get; set; }
        public ReviewModel Review { get; set; }

        public IEnumerable<SelectListItem>? ListOfCategories { get; set; }
        public IEnumerable<SelectListItem>? ListOfBrands { get; set; }
        [Display(Name = "Imagenes del Review")]
        public List<IFormFile>? ReviewImages { get; set; }
        [Display(Name = "Imagenes del Producto")]
        public List<IFormFile>? ProductImages { get; set; }
        // para la validación de las imagenes si es un review o un producto
        public bool? IsReview { get; set; }
        // para las imagenes del review o del producto
        public List<IFormFile>? Images { get; set; }
        public IEnumerable<ReviewModel>? ReviewList { get; set; }
        public IEnumerable<ReviewModel>? LastReviews { get; set; }
    }
}