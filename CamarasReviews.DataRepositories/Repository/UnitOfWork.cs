using CamarasReviews.Data;
using CamarasReviews.Repository.Interfaces;

namespace CamarasReviews.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public IReviewRepository Review { get; private set; }
    public IBrandRepository Brand { get; private set; }
    public IFeatureRepository Feature { get; private set; }
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    public IProductImageRepository ProductImage { get; private set; }
    public IReviewImageRepository ReviewImage { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Review = new ReviewRepository(_db);
        Brand = new BrandRepository(_db);
        Feature = new FeatureRepository(_db);
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
        ProductImage = new ProductImageRepository(_db);
        ReviewImage = new ReviewImageRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public void Dispose()
    {
        _db.Dispose();
        GC.SuppressFinalize(this);
    }
}
