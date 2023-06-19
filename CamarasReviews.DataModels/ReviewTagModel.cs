using CamarasReviews.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamarasReviews.Models
{
    public class ReviewTagModel
    {
        [Key]
        public Guid ReviewTagId { get; set; }
        [Required]
        public Guid ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        public ReviewModel Review { get; set; }
        [Required]
        public Guid TagId { get; set; }
        [ForeignKey("TagId")]
        public TagModel Tag { get; set; }
    }
}
