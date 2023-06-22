using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Data;
using CamarasReviews.Models;
using CamarasReviews.Repository.Interfaces;

namespace CamarasReviews.Repository
{
    public class CategoryRepository : Repository<CategoryModel>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CategoryModel category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s => s.CategoryId == category.CategoryId);
            if (objFromDb != null)
            {
                objFromDb.Name = category.Name;
                objFromDb.Description = category.Description;
            }
            _db.SaveChanges();
        }
    }
}