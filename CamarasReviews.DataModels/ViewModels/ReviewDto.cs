using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamarasReviews.Models
{
    public class ReviewDto
    {
        public Guid ReviewId { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}