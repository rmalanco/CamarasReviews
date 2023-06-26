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
            ViewBag.Categories = _unitOfWork.Category.GetAllActiveCategories();
            ViewBag.Brands = _unitOfWork.Brand.GetAllActiveBrands();
            return View();
            //ProductViewModel productViewModel = new()
            //{
            //    Product = new ProductModel(),
            //    CategoryList = _unitOfWork.Category.GetAllActiveCategories(),
            //    BrandList = _unitOfWork.Brand.GetAllActiveBrands()
            //};
            //return View(productViewModel);
        }
        #endregion

        #region Métodos de la pagina
        // Crear una marca

        #endregion

        #region Métodos de la API
        private IActionResult CreateAndUpdateProduct(ProductModel product, FeatureViewModel feature)
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
                        Description = feature.Description,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    };

                    _unitOfWork.Product.Add(product);
                    _unitOfWork.Feature.Add(featureModel);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Producto creado exitosamente." });
                }
                else
                {
                    product.ModifiedDate = DateTime.Now;
                    _unitOfWork.Product.Update(product);
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
        // Mostrar todas las marcas
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Product.GetAll(
                includeProperties: "Category,Brand"
            );
            return Json(new { data = allObj });
        }
        // Crear o editar una marca debe de se de tipo post y por api json
        [HttpPost]
        public IActionResult Create(ProductModel product, FeatureViewModel feature)
        {
            return CreateAndUpdateProduct(product, feature);
        }
        // PUT - Editar una marca, override del método Upsert
        [HttpPut]
        public IActionResult Update(ProductModel product, FeatureViewModel feature)
        {
            return CreateAndUpdateProduct(product, feature);
        }
        // Mostrar una marca
        [HttpGet]
        public IActionResult GetByID(Guid id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error mostrando la marca" });
            }
            return Json(new { success = true, data = objFromDb });
        }
        // // Eliminar una marca - desactivar
        // [HttpDelete]
        // public IActionResult Delete(Guid id)
        // {
        //     var objFromDb = _unitOfWork.Product.Get(id);
        //     if (objFromDb == null)
        //     {
        //         return Json(new { success = false, message = "Error eliminando la marca" });
        //     }
        //     _unitOfWork.Product.DisableProduct(id);
        //     _unitOfWork.Save();
        //     return Json(new { success = true, message = "Marca eliminada correctamente" });
        // }
        // // Activar una marca
        // [HttpPatch]
        // public IActionResult Active(Guid id)
        // {
        //     var objFromDb = _unitOfWork.Product.Get(id);
        //     if (objFromDb == null)
        //     {
        //         return Json(new { success = false, message = "Error activando la marca" });
        //     }
        //     _unitOfWork.Product.EnableProduct(id);
        //     _unitOfWork.Save();
        //     return Json(new { success = true, message = "Marca activada correctamente" });
        // }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
