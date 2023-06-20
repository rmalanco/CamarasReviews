using Microsoft.AspNetCore.Mvc;

namespace CamarasReviews.Areas.Reviews.Controllers
{
    [Area("Reviews")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
