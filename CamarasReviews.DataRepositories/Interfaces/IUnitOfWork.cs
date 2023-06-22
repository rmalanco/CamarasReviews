namespace CamarasReviews.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IReviewRepository Review { get; }
    IBrandRepository Brand { get; }
    IFeatureRepository Feature { get; }
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    void Save();
}
