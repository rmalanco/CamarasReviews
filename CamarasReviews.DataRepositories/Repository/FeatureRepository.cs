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
    public class FeatureRepository : Repository<FeatureModel>, IFeatureRepository
    {
        private readonly ApplicationDbContext _db;

        public FeatureRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FeatureModel feature)
        {
            var objFromDb = _db.Features.FirstOrDefault(s => s.FeatureId == feature.FeatureId);
            if (objFromDb != null)
            {
                objFromDb.Name = feature.Name;
            }
            _db.SaveChanges();
        }
    }
}