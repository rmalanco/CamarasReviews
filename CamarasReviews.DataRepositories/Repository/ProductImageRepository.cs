using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Data;
using CamarasReviews.Models;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Repository
{
    public class ProductImageRepository : Repository<ProductImageModel>, IProductImageRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void DisableImage(Guid productImageId)
        {
            var objFromDb = _db.ProductImages.FirstOrDefault(s => s.ProductImageId == productImageId);
            objFromDb.IsActive = false;
            _db.SaveChanges();
        }

        public void EnableImage(Guid productImageId)
        {
            var objFromDb = _db.ProductImages.FirstOrDefault(s => s.ProductImageId == productImageId);
            objFromDb.IsActive = true;
            _db.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetAllActiveProductImagesForDropDown()
        {
            return _db.ProductImages.Where(x => x.IsActive).Select(i => new SelectListItem()
            {
                Text = i.UrlImagen,
                Value = i.ProductImageId.ToString()
            });
        }

        public List<ProductImageModel> GetProductImagesByProductId(Guid productId)
        {
            return _db.ProductImages.Where(x => x.ProductId == productId).ToList();
        }

        public void Update(ProductImageModel productImage)
        {
            var objFromDb = _db.ProductImages.FirstOrDefault(s => s.ProductImageId == productImage.ProductImageId);
            objFromDb.UrlImagen = productImage.UrlImagen;
            objFromDb.ProductId = productImage.ProductId;
            objFromDb.IsActive = productImage.IsActive;
            _db.SaveChanges();
        }
    }
}