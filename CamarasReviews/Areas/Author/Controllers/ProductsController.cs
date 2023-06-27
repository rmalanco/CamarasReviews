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
