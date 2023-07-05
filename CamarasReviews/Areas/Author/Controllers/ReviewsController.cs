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
            var feature = _unitOfWork.Feature.GetFeatureByProductId(review.ProductId);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReviewViewModel reviewViewModel)
        {
            if (ModelState.IsValid)
            {
                // actualizamos el producto
                var product = reviewViewModel.Product;
                product.ModifiedDate = DateTime.Now;
                _unitOfWork.Product.Update(product);

                // actualizamos el review
                var review = reviewViewModel.Review;
                review.ModifiedDate = DateTime.Now;
                _unitOfWork.Review.Update(review);

                //buscamos el featureId
                var featureId = _unitOfWork.Feature.GetFeatureByProductId(product.ProductId).FeatureId;
                // actualizamos el feature
                var feature = reviewViewModel.Feature;
                feature.FeatureId = featureId;
                feature.ModifiedDate = DateTime.Now;
                _unitOfWork.Feature.Update(feature);

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
        [HttpPost]
        public IActionResult GetImagesReview(Guid reviewId)
        {
            var objFromDb = _unitOfWork.ReviewImage.GetAllActiveReviewImages(r => r.ReviewId == reviewId);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al obtener las imagenes" });
            }
            return Json(new { success = true, data = objFromDb });
        }
        [HttpPost]
        public IActionResult DeleteImageReview(Guid reviewImageId)
        {
            var objFromDb = _unitOfWork.ReviewImage.Get(reviewImageId);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al obtener la imagen" });
            }
            // eliminamos la imagen del servidor
            string rootFolder = _hostEnvironment.WebRootPath;
            string path = Path.Combine(rootFolder + objFromDb.UrlImagen.TrimStart('/'));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            // eliminamos la imagen de la base de datos
            _unitOfWork.ReviewImage.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Imagen eliminada correctamente" });
        }
        [HttpPost]
        // DeleteImageProduct
        public IActionResult DeleteImageProduct(Guid productImageId)
        {
            var objFromDb = _unitOfWork.ProductImage.Get(productImageId);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al obtener la imagen" });
            }
            // eliminamos la imagen del servidor
            string rootFolder = _hostEnvironment.WebRootPath;
            string path = Path.Combine(rootFolder + objFromDb.UrlImagen.TrimStart('/'));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            // eliminamos la imagen de la base de datos
            _unitOfWork.ProductImage.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Imagen eliminada correctamente" });
        }
        [HttpPost]
        // UploadImagesReview
        public IActionResult UploadImagesReview(Guid reviewId, List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return Json(new { success = false, message = "Error al obtener las imagenes" });
            }
            foreach (var file in files)
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
                    ReviewId = reviewId,
                    UrlImagen = "/imagenes/reviews/" + fileName,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                _unitOfWork.ReviewImage.Add(reviewImage);
            }
            _unitOfWork.Save();
            return Json(new { success = true, message = "Imagenes subidas correctamente" });
        }
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var objFromDb = _unitOfWork.Review.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al eliminar la review" });
            }
            objFromDb.IsActive = false;
            _unitOfWork.Review.Update(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Review eliminada correctamente" });
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}