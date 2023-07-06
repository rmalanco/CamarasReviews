using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Data;
using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
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

        public IEnumerable<ReviewViewModel> GetAllActiveReviewsForList()
        {
            return _db.Reviews
                .Where(r => r.IsActive)
                .Select(r => new ReviewViewModel
                {
                    Review = new ReviewModel
                    {
                        ReviewId = r.ReviewId,
                        Title = r.Title,
                        ShortDescription = r.ShortDescription,
                        CreatedDate = r.CreatedDate,
                        Author = new ApplicationUser
                        {
                            Id = r.AuthorId,
                            Name = r.Author.FullName
                        },
                        IsActive = r.IsActive,
                        ReviewImages = r.ReviewImages.Select(ri => new ReviewImageModel
                        {
                            ReviewImageId = ri.ReviewImageId,
                            UrlImagen = ri.UrlImagen
                        }).ToList()
                    },
                    Product = new ProductModel
                    {
                        Category = new CategoryModel
                        {
                            CategoryId = r.Product.Category.CategoryId,
                            Name = r.Product.Category.Name
                        }
                    }
                })
                .OrderByDescending(r => r.Review.CreatedDate)
                .ToList();
        }


        public IEnumerable<ReviewModel> GetTop5Reviews()
        {
            return _db.Reviews
            .Join(_db.ReviewImages.Where(ri => ri.IsActive),
            r => r.ReviewId,
            ri => ri.ReviewId,
            (r, ri) => new { Review = r, ReviewImage = ri })
            .GroupBy(joined => joined.Review)
            .Select(group => new ReviewModel
            {
                ReviewId = group.Key.ReviewId,
                Title = group.Key.Title,
                ShortDescription = group.Key.ShortDescription,
                LongDescription = group.Key.LongDescription,
                Pros = group.Key.Pros,
                Cons = group.Key.Cons,
                CreatedDate = group.Key.CreatedDate,
                ModifiedDate = group.Key.ModifiedDate,
                IsActive = group.Key.IsActive,
                AuthorId = group.Key.AuthorId,
                ReviewImages = new List<ReviewImageModel>
                {
                    new ReviewImageModel
                    {
                        ReviewImageId = group.Select(j => j.ReviewImage.ReviewImageId).FirstOrDefault(),
                        ReviewId = group.Key.ReviewId,
                        UrlImagen = group.Select(j => j.ReviewImage.UrlImagen).FirstOrDefault(),
                        IsActive = group.Select(j => j.ReviewImage.IsActive).FirstOrDefault()
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