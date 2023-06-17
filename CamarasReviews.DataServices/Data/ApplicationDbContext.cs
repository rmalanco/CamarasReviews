using CamarasReviews.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CamarasReviews.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<FeatureModel> Features { get; set; }
        public DbSet<ProductImageModel> ProductImages { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<ReviewCommentModel> ReviewComments { get; set; }
        public DbSet<ReviewImageModel> ReviewImages { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
        public DbSet<ReviewRatingModel> ReviewRatings { get; set; }
    }
}