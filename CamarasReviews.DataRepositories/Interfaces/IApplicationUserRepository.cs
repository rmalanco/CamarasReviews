using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamarasReviews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamarasReviews.Repository.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser applicationUser);
    }
}