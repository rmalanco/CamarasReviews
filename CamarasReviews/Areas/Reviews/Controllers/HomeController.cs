using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            ReviewViewModel reviewViewModel = new()
            {
                Review = new ReviewModel(),
                ReviewList = _unitOfWork.Review.GetAllActiveReviewsForList(r => r.IsActive),
                ListOfCategories = _unitOfWork.Category.GetAllActiveCategories(),
                LastReviews = _unitOfWork.Review.GetTop5Reviews()
            };
            return View(reviewViewModel);
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
