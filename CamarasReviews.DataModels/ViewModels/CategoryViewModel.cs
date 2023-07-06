using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamarasReviews.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
}