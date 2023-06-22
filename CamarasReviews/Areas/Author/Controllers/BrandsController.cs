using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CamarasReviews.Areas.Author.Controllers
{
    [Area("Author")]
    public class BrandsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandsController(IUnitOfWork unitOfWork)
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
        // Crear una marca

        #endregion

        #region Métodos de la API
        private IActionResult CreateAndUpdateBrand(BrandModel brand)
        {
            if (ModelState.IsValid)
            {
                if (brand.BrandId == Guid.Empty)
                {
                    brand.BrandId = Guid.NewGuid();
                    _unitOfWork.Brand.Add(brand);
                }
                else
                {
                    _unitOfWork.Brand.Update(brand);
                }
                _unitOfWork.Save();
                return Json(new { success = true, message = "Marca guardada correctamente" });
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
            var allObj = _unitOfWork.Brand.GetAll();
            return Json(new { data = allObj });
        }
        // Crear o editar una marca debe de se de tipo post y por api json
        [HttpPost]
        public IActionResult Create(BrandModel brand)
        {
            return CreateAndUpdateBrand(brand);
        }
        // PUT - Editar una marca, override del método Upsert
        [HttpPut]
        public IActionResult Update(BrandModel brand)
        {
            return CreateAndUpdateBrand(brand);
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