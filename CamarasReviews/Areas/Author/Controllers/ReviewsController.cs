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
                ListOfBrands = _unitOfWork.Brand.GetAllActiveBrands(),
                ListOfCategories = _unitOfWork.Category.GetAllActiveCategories()
            };
            return View(reviewViewModel);
        }

        public IActionResult Edit(Guid id)
        {
            var review = _unitOfWork.Review.Get(id);
            if (review == null)
            {
                return NotFound();
            }
            var product = _unitOfWork.Product.Get(review.ProductId);
            var feature = _unitOfWork.Feature.Get(review.ProductId);
            ReviewViewModel reviewViewModel = new()
            {
                Review = review,
                Product = product,
                Feature = feature,
                ListOfBrands = _unitOfWork.Brand.GetAllActiveBrands(),
                ListOfCategories = _unitOfWork.Category.GetAllActiveCategories()
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
                var AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var review = reviewViewModel.Review;
                var product = reviewViewModel.Product;
                var feature = reviewViewModel.Feature;
                // creamos primero el producto
                product.ProductId = Guid.NewGuid();
                product.CreatedDate = DateTime.Now;
                product.IsActive = true;
                // creamos el feature
                feature.FeatureId = Guid.NewGuid();
                feature.ProductId = product.ProductId;
                feature.CreatedDate = DateTime.Now;
                feature.IsActive = true;
                // creamos el review
                review.ReviewId = Guid.NewGuid();
                review.ProductId = product.ProductId;
                review.AuthorId = AuthorId;
                review.CreatedDate = DateTime.Now;
                review.IsActive = true;
                // agregamos las imagenes a el producto
                if (reviewViewModel.ProductImages != null)
                {
                    foreach (var file in reviewViewModel.ProductImages)
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
                        var productImage = new ProductImageModel
                        {
                            ProductImageId = Guid.NewGuid(),
                            ProductId = product.ProductId,
                            UrlImagen = "/imagenes/productos/" + fileName,
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        _unitOfWork.ProductImage.Add(productImage);
                    }
                }
                // agregamos las imagenes a el review
                if (reviewViewModel.ReviewImages != null)
                {
                    foreach (var file in reviewViewModel.ReviewImages)
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
                        var reviewImage = new ReviewImageModel
                        {
                            ReviewImageId = Guid.NewGuid(),
                            ReviewId = review.ReviewId,
                            UrlImagen = "/imagenes/reviews/" + fileName,
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        _unitOfWork.ReviewImage.Add(reviewImage);
                    }
                }
                _unitOfWork.Product.Add(product);
                _unitOfWork.Feature.Add(feature);
                _unitOfWork.Review.Add(review);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            reviewViewModel.ListOfBrands = _unitOfWork.Brand.GetAllActiveBrands();
            reviewViewModel.ListOfCategories = _unitOfWork.Category.GetAllActiveCategories();
            return View(reviewViewModel);
        }
        #endregion

        #region api
        [HttpGet]
        public IActionResult GetAll()
        {
            // SI ES ADMINISTRADOR
            if (User.IsInRole("Admin"))
            {
            return Json(new
            {
                data = _unitOfWork.Review.GetAllActiveReviews(r => r.IsActive)
            });
            }
            // SI ES AUTOR
            var AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Json(new
            {
                data = _unitOfWork.Review.GetAllActiveReviews(r => r.IsActive && r.AuthorId == AuthorId)
            });
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}