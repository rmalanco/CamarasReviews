using CamarasReviews.Data;
using CamarasReviews.Repository.Interfaces;

namespace CamarasReviews.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    // Aquí se agregan los repositorios que se van a utilizar
    // Ejemplo:
    // private IGenericRepository GenericRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        // Aquí se agregan los repositorios que se van a utilizar
        // Ejemplo:
        // GenericRepository = new GenericRepository(_db);
    }

    public void Dispose()
    {
        _db.SaveChanges();
    }

    public void Save()
    {
        _db.Dispose();
        GC.SuppressFinalize(this);
    }
}
