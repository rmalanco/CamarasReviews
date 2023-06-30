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
        public ReviewModel Review { get; set; }
        public ProductModel Product { get; set; }
        public FeatureModel? Feature { get; set; }
        public ProductImageModel? ProductImage { get; set; }
        public ReviewImageModel? ReviewImage { get; set; }
        [Display(Name = "Imagenes del Producto")]
        public List<IFormFile>? FilesProduct { get; set; }
        [Display(Name = "Imagenes del Review")]
        public List<IFormFile>? FilesReview { get; set; }
        // Propiedades para los listados
        public IEnumerable<SelectListItem>? ListOfCategories { get; set; }
        public IEnumerable<SelectListItem>? ListOfBrands { get; set; }
    }
}