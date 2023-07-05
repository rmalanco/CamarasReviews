using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        IEnumerable<ProductImageModel> GetAllActiveProductImages(Expression<Func<ProductImageModel, bool>> filter = null, Func<IQueryable<ProductImageModel>, IOrderedQueryable<ProductImageModel>> orderBy = null, string includeProperties = null);
    }
}