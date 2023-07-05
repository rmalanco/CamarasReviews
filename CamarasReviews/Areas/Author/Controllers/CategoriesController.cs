using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CamarasReviews.Areas.Author.Controllers
{
    [Area("Author")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Vistas de la pagina
        // Vista principal
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Métodos de la pagina
        #endregion

        #region Métodos de la API
        private IActionResult CreateAndUpdateCategory(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == Guid.Empty)
                {
                    category.CategoryId = Guid.NewGuid();
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();
                return Json(new { success = true, message = "Categoria guardada correctamente." });
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
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }
        // Crear o editar una marca debe de se de tipo post y por api json
        [HttpPost]
        public IActionResult Create(CategoryModel category)
        {
            return CreateAndUpdateCategory(category);
        }
        // PUT - Editar una marca, override del método Upsert
        [HttpPut]
        public IActionResult Update(CategoryModel category)
        {
            return CreateAndUpdateCategory(category);
        }
        // Mostrar una marca
        [HttpGet]
        public IActionResult GetByID(Guid id)
        {
            var objFromDb = _unitOfWork.Category.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al obtener la categoria." });
            }
            return Json(new { success = true, data = objFromDb });
        }
        // Eliminar una marca - desactivar
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var objFromDb = _unitOfWork.Category.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al eliminar la categoria." });
            }
            _unitOfWork.Category.DisableCategory(id);
            // _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Categoria eliminada correctamente." });
        }
        // Activar una marca
        [HttpPatch]
        public IActionResult Active(Guid id)
        {
            var objFromDb = _unitOfWork.Category.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al activar la categoria." });
            }
            _unitOfWork.Category.EnableCategory(id);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Categoria activada correctamente." });
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}