using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using CamarasReviews.Repository.Interfaces;
using CamarasReviews.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CamarasReviews.Areas.Reviews.Controllers
{
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.AuthorRole + "," + RoleConstants.UserRole)]
    [Area("Reviews")]
    public class DetailsController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Vistas de la sección Reviews
        public IActionResult Index(Guid id)
        {
            var review = _unitOfWork.Review.GetFirstOrDefault(
                r => r.ReviewId == id,
                includeProperties: "Author,ReviewImages"
                );
            if (review == null)
            {
                return RedirectToAction("Index", "Home", new { area = "Reviews" });
            }
            var product = _unitOfWork.Product.GetFirstOrDefault(
                p => p.ProductId == review.ProductId,
                includeProperties: "Brand,Category,ProductImages"
                );

            var feature = _unitOfWork.Feature.GetFeatureByProductId(review.ProductId);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _unitOfWork.ApplicationUser.GetFirstOrDefault(
                u => u.Id == userId
                ).FullName;

            ReviewViewModel reviewViewModel = new()
            {
                Review = review,
                Product = product,
                Feature = feature,
                UserId = userId,
                UserName = userName
            };
            return View(reviewViewModel);
        }
        #endregion

        #region Métodos de la sección Reviews
        #endregion

        #region API de la sección Reviews
        [HttpGet]
        public IActionResult GetComments(Guid reviewId)
        {
            var comments = _unitOfWork.Comment.GetAll(
                c => c.ReviewId == reviewId,
                orderBy: c => c.OrderByDescending(c => c.CreatedDate)
                );
            return Json(new { data = comments });
        }
        [HttpPost]
        public IActionResult PostComment(Guid reviewId, string comment)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userName = _unitOfWork.ApplicationUser.GetFirstOrDefault(
                    u => u.Id == userId
                    ).FullName;
                CommentModel commentModel = new()
                {
                    ReviewCommentId = Guid.NewGuid(),
                    Comment = comment,
                    CreatedDate = DateTime.Now,
                    ReviewId = reviewId,
                    UserId = userId,
                    UserName = userName
                };
                _unitOfWork.Comment.Add(commentModel);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Comentario agregado correctamente" });
            }
            else
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = error });
            }
        }
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