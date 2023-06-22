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
    public class BrandRepository : Repository<BrandModel>, IBrandRepository
    {
        private readonly ApplicationDbContext _db;

        public BrandRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void DisableBrand(Guid id)
        {
            var objFromDb = _db.Brands.FirstOrDefault(s => s.BrandId == id);
            if (objFromDb != null)
            {
                objFromDb.IsActive = false;
                objFromDb.DeletedDate = DateTime.Now;
            }
            _db.SaveChanges();
        }

        public void EnableBrand(Guid id)
        {
            var objFromDb = _db.Brands.FirstOrDefault(s => s.BrandId == id);
            if (objFromDb != null)
            {
                objFromDb.IsActive = true;
                objFromDb.DeletedDate = null;
            }
            _db.SaveChanges();
        }

        public void Update(BrandModel brand)
        {
            var objFromDb = _db.Brands.FirstOrDefault(s => s.BrandId == brand.BrandId);
            if (objFromDb != null)
            {
                objFromDb.Name = brand.Name;
                objFromDb.Description = brand.Description;
                objFromDb.ModifiedDate = DateTime.Now;
            }
            _db.SaveChanges();
        }
    }
}