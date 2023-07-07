using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using CamarasReviews.Utilities;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace CamarasReviews.Areas.Reviews.Controllers
{
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.AuthorRole + "," + RoleConstants.UserRole)]
    [Area("Reviews")]
    public class HomeController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Vistas de la sección Reviews
        public IActionResult Index(int? page) // ruta por pagina es: /Reviews/Home/Index?page=1
        {
            int pageNumber = page ?? 1;

            PostViewModel postViewModel = new()
            {
                Review = new ReviewModel(),
                Product = new ProductModel(),
                Categories = _unitOfWork.Category.GetAllActiveCategoriesForList(c => c.IsActive).ToList(),
                LastReviews = _unitOfWork.Review.GetTop5Reviews()
            };

            var reviews = _unitOfWork.Review.GetAllActiveReviewsForList();
            postViewModel.ReviewList = reviews.ToPagedList(pageNumber, PaginationConstants.PageSize);
            return View(postViewModel);
        }
        #endregion

        #region Métodos de la sección Reviews
        #endregion

        #region API de la sección Reviews
        #endregion

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
