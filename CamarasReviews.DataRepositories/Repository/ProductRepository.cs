using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Data;
using CamarasReviews.Models;
using CamarasReviews.Models.ViewModels;
using CamarasReviews.Repository.Interfaces;

namespace CamarasReviews.Repository
{
    public class ProductRepository : Repository<ProductModel>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ProductModel product)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.ProductId == product.ProductId);
            objFromDb.Name = product.Name;
            objFromDb.SKU = product.SKU;
            objFromDb.Description = product.Description;
            objFromDb.Price = product.Price;
            objFromDb.CategoryId = product.CategoryId;
            objFromDb.BrandId = product.BrandId;
            _db.SaveChanges();
        }

        public void DisableProduct(Guid id)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.ProductId == id);
            objFromDb.IsActive = false;
            _db.SaveChanges();
        }

        public void EnableProduct(Guid id)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.ProductId == id);
            objFromDb.IsActive = true;
            _db.SaveChanges();
        }

        // método para mostrar el producto, con su categoria, marca y feature description
        public ProductViewModel GetProduct(Guid id)
        {
            //realizar un inner join de las tablas para obtener el producto con su categoria, marca y feature description
            var product = _db.Products.Join(_db.Categories, p => p.CategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Join(_db.Brands, p => p.p.BrandId, b => b.BrandId, (p, b) => new { p, b })
                .Join(_db.Features, p => p.p.p.ProductId, pf => pf.ProductId, (p, pf) => new { p, pf })
                .Where(p => p.p.p.p.ProductId == id)
                .Select(p => new ProductViewModel
                {
                    ProductId = p.p.p.p.ProductId,
                    Name = p.p.p.p.Name,
                    SKU = p.p.p.p.SKU,
                    Description = p.p.p.p.Description,
                    Price = p.p.p.p.Price,
                    IsActive = p.p.p.p.IsActive,
                    CategoryId = p.p.p.p.CategoryId,
                    Category = p.p.p.c,
                    BrandId = p.p.b.BrandId,
                    Brand = p.p.b,
                    FeatureDescription = p.pf.Description
                }).FirstOrDefault();
            return product;
        }
    }
}