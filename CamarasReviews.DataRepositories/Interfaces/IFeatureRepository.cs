using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IFeatureRepository : IRepository<FeatureModel>
    {
        void Update(FeatureModel feature);
        FeatureModel GetFeatureByProductId(Guid productId);
    }
}