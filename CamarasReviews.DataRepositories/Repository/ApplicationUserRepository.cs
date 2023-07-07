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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser applicationUser)
        {
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(s => s.Id == applicationUser.Id);
            objFromDb.Name = applicationUser.Name;
            objFromDb.LastName = applicationUser.LastName;
            objFromDb.PhoneNumber = applicationUser.PhoneNumber;
            objFromDb.Address = applicationUser.Address;
            objFromDb.City = applicationUser.City;
            objFromDb.Country = applicationUser.Country;
            _db.SaveChanges();
        }
    }
}