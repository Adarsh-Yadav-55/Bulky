using Bulky.DataAccess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public class CategoryRepositary : Repositary<Category>, ICategoryRepositary
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepositary(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();  
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
