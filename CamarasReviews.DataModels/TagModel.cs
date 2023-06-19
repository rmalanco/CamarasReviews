using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamarasReviews.Models
{
    public class TagModel
    {
        [Key]
        public Guid TagId { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<ReviewTagModel> ReviewTags { get; set; }        
    }
}
