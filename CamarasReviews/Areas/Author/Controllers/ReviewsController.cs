using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CamarasReviews.Areas.Author.Controllers
{
    [Area("Author")]
    public class ReviewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ReviewsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        #region vistas para los reviews
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ReviewViewModel reviewViewModel = new()
            {
                Review = new ReviewModel(),
                Product = new ProductModel(),
                Feature = new FeatureModel(),
                ProductImage = new ProductImageModel(),
                ReviewImage = new ReviewImageModel(),
                ListOfCategories = _unitOfWork.Category.GetAllActiveCategories(),
                ListOfBrands = _unitOfWork.Brand.GetAllActiveBrands()
            };
            return View(reviewViewModel);
        }
        #endregion

        #region métodos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
                if (reviewViewModel.Product.ProductId == Guid.Empty)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    // Producto
                    reviewViewModel.Product.ProductId = Guid.NewGuid();
                    reviewViewModel.Product.CreatedDate = DateTime.Now;
                    reviewViewModel.Product.IsActive = true;

                    // Caracteristica
                    reviewViewModel.Feature.FeatureId = Guid.NewGuid();
                    reviewViewModel.Feature.ProductId = reviewViewModel.Product.ProductId;
                    reviewViewModel.Feature.CreatedDate = DateTime.Now;
                    reviewViewModel.Feature.IsActive = true;

                    // imagenes del producto
                    if (reviewViewModel.FilesProduct != null)
                    {
                        foreach (var file in reviewViewModel.FilesProduct)
                        {
                            string rootFolder = _hostEnvironment.WebRootPath;
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            fileName = Guid.NewGuid() + extension;
                            string path = Path.Combine(rootFolder + "/imagenes/productos/", fileName);
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }
                            reviewViewModel.ProductImage.ProductImageId = Guid.NewGuid();
                            reviewViewModel.ProductImage.ProductId = reviewViewModel.Product.ProductId;
                            reviewViewModel.ProductImage.UrlImagen = @"/imagenes/productos/" + fileName;
                            reviewViewModel.ProductImage.CreatedDate = DateTime.Now;
                            reviewViewModel.ProductImage.IsActive = true;

                            _unitOfWork.ProductImage.Add(reviewViewModel.ProductImage);
                        }
                    }

                    // review
                    reviewViewModel.Review.ReviewId = Guid.NewGuid();
                    reviewViewModel.Review.ProductId = reviewViewModel.Product.ProductId;
                    reviewViewModel.Review.AuthorId = userId;
                    reviewViewModel.Review.CreatedDate = DateTime.Now;
                    reviewViewModel.Review.IsActive = true;

                    // imagenes de la review
                    if (reviewViewModel.FilesReview != null)
                    {
                        foreach (var file in reviewViewModel.FilesReview)
                        {
                            string rootFolder = _hostEnvironment.WebRootPath;
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            fileName = Guid.NewGuid() + extension;
                            string path = Path.Combine(rootFolder + "/imagenes/reviews/", fileName);
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }
                            reviewViewModel.ReviewImage.ReviewImageId = Guid.NewGuid();
                            reviewViewModel.ReviewImage.ReviewId = reviewViewModel.Review.ReviewId;
                            reviewViewModel.ReviewImage.UrlImagen = @"/imagenes/reviews/" + fileName;
                            reviewViewModel.ReviewImage.CreatedDate = DateTime.Now;
                            reviewViewModel.ReviewImage.IsActive = true;

                            _unitOfWork.ReviewImage.Add(reviewViewModel.ReviewImage);
                        }
                    }

                    _unitOfWork.Product.Add(reviewViewModel.Product);
                    _unitOfWork.Feature.Add(reviewViewModel.Feature);
                    _unitOfWork.Review.Add(reviewViewModel.Review);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
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