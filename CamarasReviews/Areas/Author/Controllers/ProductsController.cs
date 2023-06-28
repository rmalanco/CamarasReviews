using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CamarasReviews.Areas.Author.Controllers
{
    [Area("Author")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        #region Vistas de la pagina
        // Vista principal
        public IActionResult Index()
        {
            //ViewBag.Categories = _unitOfWork.Category.GetAllActiveCategories();
            //ViewBag.Brands = _unitOfWork.Brand.GetAllActiveBrands();
            //return View();
            ProductViewModel productViewModel = new ProductViewModel()
            {
                ListOfCategories = _unitOfWork.Category.GetAllActiveCategories(),
                ListOfBrands = _unitOfWork.Brand.GetAllActiveBrands()
            };
            return View(productViewModel);
        }
        #endregion

        #region Métodos de la pagina
        // Crear una producto

        #endregion

        #region Métodos de la API
        private IActionResult CreateAndUpdateProduct(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductId == Guid.Empty)
                {
                    product.ProductId = Guid.NewGuid();
                    product.CreatedDate = DateTime.Now;
                    product.IsActive = true;

                    var featureModel = new FeatureModel
                    {
                        FeatureId = Guid.NewGuid(),
                        ProductId = product.ProductId,
                        Description = product.FeatureDescription,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    };

                    if (product.Files != null)
                    {
                        foreach (var file in product.Files)
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
                                UrlImagen = @"/imagenes/productos/" + fileName,
                                CreatedDate = DateTime.Now,
                                IsActive = true
                            };
                            _unitOfWork.ProductImage.Add(productImage);
                        }
                    }
                    else
                    {
                        var productImage = new ProductImageModel
                        {
                            ProductImageId = Guid.NewGuid(),
                            ProductId = product.ProductId,
                            UrlImagen = @"/imagenes/productos/default.png",
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        _unitOfWork.ProductImage.Add(productImage);
                    }

                    _unitOfWork.Product.Add(product);
                    _unitOfWork.Feature.Add(featureModel);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Producto creado exitosamente." });
                }
                else
                {
                    var productFromDb = _unitOfWork.Product.Get(product.ProductId);
                    var featureFromDb = _unitOfWork.Feature.GetFeatureByProductId(product.ProductId);
                    
                    productFromDb.Name = product.Name;
                    productFromDb.SKU = product.SKU;
                    productFromDb.Description = product.Description;
                    productFromDb.Price = product.Price;
                    productFromDb.CategoryId = product.CategoryId;
                    productFromDb.BrandId = product.BrandId;
                    productFromDb.ModifiedDate = DateTime.Now;

                    featureFromDb.Description = product.FeatureDescription;
                    featureFromDb.ModifiedDate = DateTime.Now;
                    // Actualizar imágenes del producto
                    if (product.Files != null && product.Files.Count > 0)
                    {
                        // Eliminar imágenes existentes
                        var productImagesFromDb = _unitOfWork.ProductImage.GetProductImagesByProductId(product.ProductId);
                        foreach (var productImage in productImagesFromDb)
                        {
                            //string rootFolder = _hostEnvironment.WebRootPath;
                            //string path = Path.Combine(rootFolder + productImage.UrlImagen.TrimStart('~'));
                            //if (System.IO.File.Exists(path))
                            //{
                            //    System.IO.File.Delete(path);
                            //}
                            _unitOfWork.ProductImage.DisableImage(productImage.ProductImageId);
                        }

                        // Agregar nuevas imágenes
                        foreach (var file in product.Files)
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

                    _unitOfWork.Product.Update(productFromDb);
                    _unitOfWork.Feature.Update(featureFromDb);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Producto actualizado exitosamente." });
                }
            }
            else
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = error });
            }
        }
        // Mostrar todos los productos activos
        [HttpGet]
        public IActionResult GetAllActiveProducts()
        {
            var allObj = _unitOfWork.Product.GetAll(
                s => s.IsActive,
                orderBy: s => s.OrderByDescending(s => s.CreatedDate),
                includeProperties: "Category,Brand"
                );
            return Json(new { data = allObj });
        }
        // Post - Crear un producto
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            return CreateAndUpdateProduct(product);
        }
        // PUT - Editar un producto
        [HttpPut]
        public IActionResult Update(ProductViewModel product)
        {
            return CreateAndUpdateProduct(product);
        }
        // Mostrar un producto por ID
        [HttpGet]
        public IActionResult GetProductById(Guid id)
        {
            var objFromDb = _unitOfWork.Product.GetProduct(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error obteniendo el producto" });
            }
            return Json(new { success = true, data = objFromDb });
        }
        // Eliminar un producto - desactivar
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error eliminando el producto" });
            }
            _unitOfWork.Product.DisableProduct(id);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Producto eliminado correctamente" });
        }
        // Activar un producto
        [HttpPatch]
        public IActionResult Active(Guid id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error activando el producto" });
            }
            _unitOfWork.Product.EnableProduct(id);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Producto activado correctamente" });
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
