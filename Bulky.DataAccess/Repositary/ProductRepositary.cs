using Bulky.DataAccess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public class ProductRepositary : Repositary<Product>, IProductRepositary
    {
        private readonly ApplicationDbContext _db;
        public ProductRepositary(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            var ProductFromDb = _db.Products.FirstOrDefault(p => p.Id == product.Id);
            if (ProductFromDb != null)
            {
                ProductFromDb.Title = product.Title;
                ProductFromDb.ISBN = product.ISBN;
                ProductFromDb.Description = product.Description;
                ProductFromDb.CategoryId = product.CategoryId;
                ProductFromDb.ListPrice = product.ListPrice;
                ProductFromDb.Price = product.Price;
                ProductFromDb.Price50 = product.Price50;
                ProductFromDb.Price100 = product.Price100;
                ProductFromDb.Author = product.Author;
                if (product.ImageUrl != null)
                {
                    ProductFromDb.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
