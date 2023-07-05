using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Data;
using CamarasReviews.Models;
using CamarasReviews.Repository.Interfaces;

namespace CamarasReviews.Repository
{
    public class ReviewImageRepository : Repository<ReviewImageModel>, IReviewImageRepository
    {
        private readonly ApplicationDbContext _db;

        public ReviewImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<ReviewImageModel> GetAllActiveReviewImages(Expression<Func<ReviewImageModel, bool>> filter = null, Func<IQueryable<ReviewImageModel>, IOrderedQueryable<ReviewImageModel>> orderBy = null, string includeProperties = null)
        {
            IQueryable<ReviewImageModel> query = _db.ReviewImages.Where(s => s.IsActive);
            return base.GetAll(filter, orderBy, includeProperties);
        }

        public void Update(ReviewImageModel reviewImageModel)
        {
            var objFromDb = _db.ReviewImages.FirstOrDefault(s => s.ReviewImageId == reviewImageModel.ReviewImageId);
            objFromDb.UrlImagen = reviewImageModel.UrlImagen;
            objFromDb.ReviewId = reviewImageModel.ReviewId;
            objFromDb.IsActive = reviewImageModel.IsActive;
            _db.SaveChanges();
        }
    }
}