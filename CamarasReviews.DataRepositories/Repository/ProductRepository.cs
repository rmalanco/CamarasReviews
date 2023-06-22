using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CamarasReviews.Data;
using CamarasReviews.Models;
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
        public IEnumerable<ProductModel> FindWithCategoryAndBrand(Func<ProductModel, bool> predicate)
        {
            var products = _db.Products.Join(_db.Categories, p => p.CategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Join(_db.Brands, pc => pc.p.BrandId, b => b.BrandId, (pc, b) => new { pc, b })
                .Where(pc => predicate(pc.pc.p))
                .Select(pc => new ProductModel
                {
                    ProductId = pc.pc.p.ProductId,
                    Name = pc.pc.p.Name,
                    SKU = pc.pc.p.SKU,
                    Description = pc.pc.p.Description,
                    Price = pc.pc.p.Price,
                    CategoryId = pc.pc.p.CategoryId,
                    Category = pc.pc.c,
                    BrandId = pc.pc.p.BrandId,
                    Brand = pc.b
                }).ToList();

            return products;
        }

        public IEnumerable<ProductModel> GetAllWithCategoryAndBrand()
        {
            var products = _db.Products.Join(_db.Categories, p => p.CategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Join(_db.Brands, pc => pc.p.BrandId, b => b.BrandId, (pc, b) => new { pc, b })
                .Select(pc => new ProductModel
                {
                    ProductId = pc.pc.p.ProductId,
                    Name = pc.pc.p.Name,
                    SKU = pc.pc.p.SKU,
                    Description = pc.pc.p.Description,
                    Price = pc.pc.p.Price,
                    CategoryId = pc.pc.p.CategoryId,
                    Category = pc.pc.c,
                    BrandId = pc.pc.p.BrandId,
                    Brand = pc.b
                }).ToList();

            return products;
        }

        public IEnumerable<ProductModel> GetAllWithCategoryAndBrandByBrand(Guid brandId)
        {
            var products = _db.Products.Join(_db.Categories, p => p.CategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Join(_db.Brands, pc => pc.p.BrandId, b => b.BrandId, (pc, b) => new { pc, b })
                .Where(pc => pc.b.BrandId == brandId)
                .Select(pc => new ProductModel
                {
                    ProductId = pc.pc.p.ProductId,
                    Name = pc.pc.p.Name,
                    SKU = pc.pc.p.SKU,
                    Description = pc.pc.p.Description,
                    Price = pc.pc.p.Price,
                    CategoryId = pc.pc.p.CategoryId,
                    Category = pc.pc.c,
                    BrandId = pc.pc.p.BrandId,
                    Brand = pc.b
                }).ToList();
            return products;
        }

        public IEnumerable<ProductModel> GetAllWithCategoryAndBrandByCategory(Guid categoryId)
        {
            var products = _db.Products.Join(_db.Categories, p => p.CategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Join(_db.Brands, pc => pc.p.BrandId, b => b.BrandId, (pc, b) => new { pc, b })
                .Where(pc => pc.pc.c.CategoryId == categoryId)
                .Select(pc => new ProductModel
                {
                    ProductId = pc.pc.p.ProductId,
                    Name = pc.pc.p.Name,
                    SKU = pc.pc.p.SKU,
                    Description = pc.pc.p.Description,
                    Price = pc.pc.p.Price,
                    CategoryId = pc.pc.p.CategoryId,
                    Category = pc.pc.c,
                    BrandId = pc.pc.p.BrandId,
                    Brand = pc.b
                }).ToList();
            return products;
        }

        public ProductModel GetWithCategoryAndBrand(Guid id)
        {
            var product = _db.Products.Join(_db.Categories, p => p.CategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Join(_db.Brands, pc => pc.p.BrandId, b => b.BrandId, (pc, b) => new { pc, b })
                .Where(pc => pc.pc.p.ProductId == id)
                .Select(pc => new ProductModel
                {
                    ProductId = pc.pc.p.ProductId,
                    Name = pc.pc.p.Name,
                    SKU = pc.pc.p.SKU,
                    Description = pc.pc.p.Description,
                    Price = pc.pc.p.Price,
                    CategoryId = pc.pc.p.CategoryId,
                    Category = pc.pc.c,
                    BrandId = pc.pc.p.BrandId,
                    Brand = pc.b
                }).FirstOrDefault();
            return product;
        }

        public ProductModel GetWithCategoryAndBrandBySKU(string sku)
        {
            var product = _db.Products.Join(_db.Categories, p => p.CategoryId, c => c.CategoryId, (p, c) => new { p, c })
                .Join(_db.Brands, pc => pc.p.BrandId, b => b.BrandId, (pc, b) => new { pc, b })
                .Where(pc => pc.pc.p.SKU == sku)
                .Select(pc => new ProductModel
                {
                    ProductId = pc.pc.p.ProductId,
                    Name = pc.pc.p.Name,
                    SKU = pc.pc.p.SKU,
                    Description = pc.pc.p.Description,
                    Price = pc.pc.p.Price,
                    CategoryId = pc.pc.p.CategoryId,
                    Category = pc.pc.c,
                    BrandId = pc.pc.p.BrandId,
                    Brand = pc.b
                }).FirstOrDefault();
            return product;
        }
    }
}