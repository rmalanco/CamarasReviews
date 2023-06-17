namespace CamarasReviews.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void Save(); // metodo para guardar los cambios en la base de datos

    // Aquí se agregan los repositorios que se van a utilizar
    // Ejemplo:
    // IGenericRepository<BrandModel> BrandRepository { get; }
}
