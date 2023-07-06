using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IReviewRepository : IRepository<ReviewModel>
    {
        void Update(ReviewModel review);
        IEnumerable<ReviewDto> GetAllActiveReviews(Expression<Func<ReviewModel, bool>> condition);
        IEnumerable<SelectListItem> GetTop5ReviewsAndImage();
        IEnumerable<ReviewViewModel> GetAllActiveReviewsForList();
        IEnumerable<ReviewModel> GetTop5Reviews();
    }
}