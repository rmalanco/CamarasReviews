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
        // Crear una marca

        #endregion

        #region Métodos de la API
        private IActionResult CreateAndUpdateBrand(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                //     if (product.ProductId == Guid.Empty)
                //     {
                //         product.ProductId = Guid.NewGuid();
                //         _unitOfWork.Product.Add(product);
                //     }
                //     else
                //     {
                //         _unitOfWork.Product.Update(product);
                //     }
                //     _unitOfWork.Save();
                return Json(new { success = true, message = "El producto se ha guardado correctamente" });
            }
            else
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = error });
            }

            // if (ModelState.IsValid)
            // {
            //     if (product.ProductId == Guid.Empty)
            //     {
            //         // product.ProductId = Guid.NewGuid();
            //         // _unitOfWork.Product.Add(product);
            //     }
            //     else
            //     {
            //         // _unitOfWork.Product.Update(product);
            //     }
            //     // _unitOfWork.Save();
            //     return Json(new { success = true, message = "El producto se ha guardado correctamente" });
            // }
            // else
            // {
            //     var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            //     return Json(new { success = false, message = error });
            // }
        }
        // Mostrar todas las marcas
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Product.GetAll();
            return Json(new { data = allObj });
        }
        // Crear o editar una marca debe de se de tipo post y por api json
        [HttpPost]
        public IActionResult Create(ProductModel product)
        {
            return CreateAndUpdateBrand(product);
        }
        // PUT - Editar una marca, override del método Upsert
        [HttpPut]
        public IActionResult Update(ProductModel product)
        {
            return CreateAndUpdateBrand(product);
        }
        // Mostrar una marca
        [HttpGet]
        public IActionResult GetByID(Guid id)
        {
            var objFromDb = _unitOfWork.Brand.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error mostrando la marca" });
            }
            return Json(new { success = true, data = objFromDb });
        }
        // Eliminar una marca - desactivar
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var objFromDb = _unitOfWork.Brand.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error eliminando la marca" });
            }
            _unitOfWork.Brand.DisableBrand(id);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Marca eliminada correctamente" });
        }
        // Activar una marca
        [HttpPatch]
        public IActionResult Active(Guid id)
        {
            var objFromDb = _unitOfWork.Brand.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error activando la marca" });
            }
            _unitOfWork.Brand.EnableBrand(id);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Marca activada correctamente" });
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
