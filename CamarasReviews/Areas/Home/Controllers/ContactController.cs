using Microsoft.AspNetCore.Mvc;

namespace CamarasReviews.Areas.Home.Controllers
{
    [Area("Home")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
