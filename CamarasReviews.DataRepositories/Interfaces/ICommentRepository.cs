using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Repository.Interfaces
{
    public interface ICommentRepository : IRepository<CommentModel>
    {
        void Update(CommentModel comment);
    }
}