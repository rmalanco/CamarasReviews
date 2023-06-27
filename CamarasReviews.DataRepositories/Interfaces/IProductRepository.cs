using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IProductRepository : IRepository<ProductModel>
    {
        void Update(ProductModel product);
        void DisableProduct(Guid id);
        void EnableProduct(Guid id);
        ProductViewModel GetProduct(Guid id);
    }
}