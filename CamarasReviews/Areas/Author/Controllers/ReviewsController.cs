using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CamarasReviews.Areas.Author.Controllers
{
    [Area("Author")]
    public class ReviewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region vistas para los reviews
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var viewModel = new ReviewViewModel
            {
                Brands = _unitOfWork.Brand.GetAll().ToList(),
                Categories = _unitOfWork.Category.GetAll().ToList()
            };

            return View(viewModel);
        }
        #endregion

        #region métodos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
               try
                {
                    var review = new ReviewModel
                    {
                        Title = reviewViewModel.Title,
                        LongDescription = reviewViewModel.LongDescription,
                        ProductId = reviewViewModel.ProductId,
                        CreatedDate = DateTime.Now,
                    };

                    _unitOfWork.Review.Add(review);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(reviewViewModel);
        }
        #endregion

        #region api
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}