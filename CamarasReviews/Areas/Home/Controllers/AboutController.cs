using Microsoft.AspNetCore.Mvc;

namespace CamarasReviews.Areas.Home.Controllers
{
    [Area("Home")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
