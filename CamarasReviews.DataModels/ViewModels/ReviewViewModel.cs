using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Models.ViewModels
{
    public class ReviewViewModel : ReviewModel
    {
        public ReviewModel Review { get; set; }
        public ProductModel Product { get; set; }
        public FeatureModel Feature { get; set; }
        // Propiedades para los listados
        public int BrandId { get; set; }
        public List<BrandModel> Brands { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryModel> Categories { get; set; }
        [Display(Name = "Imágenes del Producto")]
        public List<ProductImageModel> ProductImages { get; set; }
        [Display(Name = "Imágenes del Review")]
        public List<ReviewImageModel> ReviewImages { get; set; }
    }
}