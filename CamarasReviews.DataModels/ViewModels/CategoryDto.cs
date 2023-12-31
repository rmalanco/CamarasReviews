using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamarasReviews.Models.ViewModels
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryModel>? Category { get; set; }
    }
}