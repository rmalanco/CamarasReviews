using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Models;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IReviewImageRepository : IRepository<ReviewImageModel>
    {
        void Update(ReviewImageModel reviewImageModel);
        IEnumerable<ReviewImageModel> GetAllActiveReviewImages(Expression<Func<ReviewImageModel, bool>> filter = null, Func<IQueryable<ReviewImageModel>, IOrderedQueryable<ReviewImageModel>> orderBy = null, string includeProperties = null);
    }
}