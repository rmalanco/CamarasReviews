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