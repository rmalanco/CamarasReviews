using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImageModel>
    {
        IEnumerable<SelectListItem> GetAllActiveProductImagesForDropDown();
        void Update(ProductImageModel productImage);
        List<ProductImageModel> GetProductImagesByProductId(Guid productId);
        void DisableImage(Guid productImageId);
        void EnableImage(Guid productImageId);
    }
}