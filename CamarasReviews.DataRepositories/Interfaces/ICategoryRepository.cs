using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<CategoryModel>
    {
        void Update(CategoryModel category);
        // desactivar una marca
        void DisableCategory(Guid id);
        // activar una marca
        void EnableCategory(Guid id);
        // obtener todas las marcas
        IEnumerable<SelectListItem> GetAllCategories();
        // obtener todas las marcas activas
        IEnumerable<SelectListItem> GetAllActiveCategories();
        // obtener todas las marcas desactivadas
        IEnumerable<SelectListItem> GetAllInactiveCategories();
    }
}