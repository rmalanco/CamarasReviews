using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Data;
using CamarasReviews.Models;
using CamarasReviews.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Repository
{
    public class CategoryRepository : Repository<CategoryModel>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void DisableCategory(Guid id)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s => s.CategoryId == id);
            if (objFromDb != null)
            {
                objFromDb.IsActive = false;
                objFromDb.DeletedDate = DateTime.Now;
            }
            _db.SaveChanges();
        }

        public void EnableCategory(Guid id)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s => s.CategoryId == id);
            if (objFromDb != null)
            {
                objFromDb.IsActive = true;
                objFromDb.DeletedDate = null;
            }
            _db.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetAllActiveCategories()
        {
            return _db.Categories.Where(s => s.IsActive == true).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.CategoryId.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetAllCategories()
        {
            return _db.Categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.CategoryId.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetAllInactiveCategories()
        {
            return _db.Categories.Where(s => s.IsActive == false).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.CategoryId.ToString()
            });
        }

        public void Update(CategoryModel category)
        {
            var objFromDb = _db.Categories.FirstOrDefault(s => s.CategoryId == category.CategoryId);
            if (objFromDb != null)
            {
                objFromDb.Name = category.Name;
                objFromDb.ModifiedDate = DateTime.Now;
            }
            _db.SaveChanges();
        }
    }
}