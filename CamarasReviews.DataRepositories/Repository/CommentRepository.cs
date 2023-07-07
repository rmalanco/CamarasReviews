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
    public class CommentRepository : Repository<CommentModel>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;

        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CommentModel comment)
        {
            var objFromDb = _db.Comments.FirstOrDefault(s => s.ReviewCommentId == comment.ReviewCommentId);
            objFromDb.Comment = comment.Comment;
            objFromDb.CreatedDate = comment.CreatedDate;
            objFromDb.ReviewId = comment.ReviewId;
            objFromDb.UserId = comment.UserId;
            _db.SaveChanges();
        }
    }
}