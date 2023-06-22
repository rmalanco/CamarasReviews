using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;

namespace CamarasReviews.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<CategoryModel>
    {
        void Update(CategoryModel category);
    }
}