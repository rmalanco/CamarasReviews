using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<SelectListItem> ListOfReviews { get; set; }
    }
}