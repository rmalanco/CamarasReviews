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
    public class ReviewRepository : Repository<ReviewModel>, IReviewRepository
    {
        private readonly ApplicationDbContext _db;

        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<ReviewDto> GetAllActiveReviews(Expression<Func<ReviewModel, bool>> condition)
        {
            return _db.Reviews
                .Where(condition)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    Title = r.Title,
                    CreatedDate = r.CreatedDate,
                    Author = new ApplicationUser
                    {
                        Id = r.AuthorId,
                        Email = r.Author.Email
                    }.Email,
                    IsActive = r.IsActive
                })
                .ToList();
        }

        public IEnumerable<ReviewModel> GetAllActiveReviewsForList(Expression<Func<ReviewModel, bool>> condition)
        {
            return _db.Reviews
                .Where(condition)
                .ToList();
        }

        public IEnumerable<ReviewModel> GetTop5Reviews()
        {
            return _db.Reviews
            .Join(_db.ReviewImages.Where(ri => ri.IsActive),
                r => r.ReviewId,
                ri => ri.ReviewId,
                (r, ri) => new ReviewModel
                {
                    ReviewId = r.ReviewId,
                    Title = r.Title,
                    ShortDescription = r.ShortDescription,
                    LongDescription = r.LongDescription,
                    Pros = r.Pros,
                    Cons = r.Cons,
                    CreatedDate = r.CreatedDate,
                    ModifiedDate = r.ModifiedDate,
                    IsActive = r.IsActive,
                    AuthorId = r.AuthorId,
                    ReviewImages = new List<ReviewImageModel>
                    {
                        new ReviewImageModel
                        {
                            ReviewImageId = ri.ReviewImageId,
                            ReviewId = ri.ReviewId,
                            UrlImagen = ri.UrlImagen,
                            IsActive = ri.IsActive
                        }
                    }
                })
            .OrderByDescending(r => r.CreatedDate)
            .Take(5)
            .ToList();
        }

        public IEnumerable<SelectListItem> GetTop5ReviewsAndImage()
        {
            var reviewImage = _db.ReviewImages
                .Join(_db.Reviews.Where(r => r.IsActive),  // Filtrar solo las revisiones activas
                    ri => ri.ReviewId,
                    r => r.ReviewId,
                    (ri, _) => new
                    {
                        ri.ReviewId,
                        ri.UrlImagen
                    })
                .GroupBy(ri => ri.ReviewId)  // Agrupar por ReviewId
                .Select(g => g.First())  // Seleccionar la primera imagen de cada grupo
                .ToList();

            // mapeo de la lista de reviews a una lista de SelectListItem
            return reviewImage.Select(r => new SelectListItem
            {
                Text = r.UrlImagen,
                Value = r.ReviewId.ToString()
            });
        }

        public void Update(ReviewModel review)
        {
            var objDesdeDb = _db.Reviews.FirstOrDefault(s => s.ReviewId == review.ReviewId);
            objDesdeDb.Title = review.Title;
            objDesdeDb.ShortDescription = review.ShortDescription;
            objDesdeDb.LongDescription = review.LongDescription;
            objDesdeDb.Pros = review.Pros;
            objDesdeDb.Cons = review.Cons;
            objDesdeDb.ModifiedDate = DateTime.Now;
            objDesdeDb.IsActive = review.IsActive;
            _db.SaveChanges();
        }
    }
}