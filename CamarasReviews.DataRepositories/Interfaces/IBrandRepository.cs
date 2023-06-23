using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IBrandRepository : IRepository<BrandModel>
    {
        void Update(BrandModel brand);
        // desactivar una marca
        void DisableBrand(Guid id);
        // activar una marca
        void EnableBrand(Guid id);
        // obtener todas las marcas 
        IEnumerable<SelectListItem> GetAllBrands();
        // obtener todas las marcas activas
        IEnumerable<SelectListItem> GetAllActiveBrands();
        // obtener todas las marcas desactivadas
        IEnumerable<SelectListItem> GetAllDisabledBrands();
    }
}