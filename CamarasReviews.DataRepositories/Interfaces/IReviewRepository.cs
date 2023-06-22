using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IReviewRepository : IRepository<ReviewModel>
    {
        void Update(ReviewModel review);
    }
}