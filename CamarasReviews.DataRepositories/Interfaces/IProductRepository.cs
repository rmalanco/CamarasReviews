using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IProductRepository : IRepository<ProductModel>
    {
        // IEnumerable<ProductModel> GetAllWithCategoryAndBrand(); // método para obtener todos los productos con sus categorías y marcas
        // IEnumerable<ProductModel> FindWithCategoryAndBrand(Func<ProductModel, bool> predicate); // método para obtener todos los productos con sus categorías y marcas que cumplan con un predicado
        // IEnumerable<ProductModel> GetAllWithCategoryAndBrandByCategory(Guid categoryId); // método para obtener todos los productos con sus categorías y marcas que pertenezcan a una categoría
        // IEnumerable<ProductModel> GetAllWithCategoryAndBrandByBrand(Guid brandId); // método para obtener todos los productos con sus categorías y marcas que pertenezcan a una marca
        // ProductModel GetWithCategoryAndBrand(Guid id); // método para obtener un producto con su categoría y marca
        // ProductModel GetWithCategoryAndBrandBySKU(string sku); // método para obtener un producto con su categoría y marca por su SKU
        void Update(ProductModel product);
    }
}