using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Models.ViewModels
{
    public class PostViewModel
    {
        public ReviewModel Review { get; set; }
        public ProductModel Product { get; set; }
        public FeatureModel Feature { get; set; }
        public IEnumerable<ReviewModel>? ReviewList { get; set; }
        public IEnumerable<ReviewModel>? LastReviews { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}