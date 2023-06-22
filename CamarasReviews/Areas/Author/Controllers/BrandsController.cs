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
    public class BrandsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Vistas de la pagina
        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Métodos de la pagina
        // Crear una marca

        #endregion

        #region Métodos de la API
        // Mostrar todas las marcas
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Brand.GetAll();
            return Json(new { data = allObj });
        }
        // Borrar una marca
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var objFromDb = _unitOfWork.Brand.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando la marca" });
            }
            _unitOfWork.Brand.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Marca borrada correctamente" });
        }
        // Crear o editar una marca debe de se de tipo post y por api json
        [HttpPost]
        public IActionResult Upsert(BrandModel brand)
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
                return Json(new { success = true, message = "Operación exitosa" });
            }
            else
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Error en la operación", errors = error });
            }
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}