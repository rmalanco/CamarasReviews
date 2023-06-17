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
        // Aqui se agregan los DbSet para cada modelo que se quiera agregar a la base de datos
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<BrandModel> Brands { get; set; }
        DbSet<CategoryModel> Categories { get; set; }
        DbSet<FeatureModel> Features { get; set; }
        DbSet<ProductImageModel> ProductImages { get; set; }
        DbSet<ProductModel> Products { get; set; }
        DbSet<RatingModel> Ratings { get; set; }
        DbSet<ReviewCommentModel> ReviewComments { get; set; }
        DbSet<ReviewImageModel> ReviewImages { get; set; }
        DbSet<ReviewModel> Reviews { get; set; }
        DbSet<ReviewRatingModel> ReviewRatings { get; set; }
    }
}