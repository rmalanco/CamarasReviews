using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CamarasReviews.Areas.Reviews.Controllers
{
    [Area("Reviews")]
    public class HomeController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        #endregion

        #region Constructor
        public HomeController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        #endregion

        #region Vistas de la sección Reviews
        public IActionResult Index(int? page) // ruta por pagina es: /Reviews/Home/Index?page=1
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;
            
            PostViewModel postViewModel = new()
            {
                Review = new ReviewModel(),
                Product = new ProductModel(),
                //ReviewList = _unitOfWork.Review.GetAllActiveReviewsForList(),
                Categories = _unitOfWork.Category.GetAllActiveCategoriesForList(c => c.IsActive).ToList(),
                LastReviews = _unitOfWork.Review.GetTop5Reviews()
            };

            var reviews = _unitOfWork.Review.GetAllActiveReviewsForList();
            postViewModel.ReviewList = reviews.ToPagedList(pageNumber, pageSize);
            return View(postViewModel);
        }
        #endregion

        #region Métodos de la sección Reviews
        #endregion

        #region API de la sección Reviews
        #endregion

        #region Error
        #endregion
    }
}
