using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Models;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IReviewRepository : IRepository<ReviewModel>
    {
        void Update(ReviewModel review);
        IEnumerable<ReviewDto> GetAllActiveReviews(Expression<Func<ReviewModel, bool>> condition);
    }
}