using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepositary Category { get; private set; }
        public IProductRepositary Product { get; private set; }
        public ICompanyRepositary Company { get; private set; }
        public IShoppingCartRepositary ShoppingCart { get; private set; }
        public IApplicationUserRepositary ApplicationUser { get; private set; }
        public IOrderDetailRepositary OrderDetail { get; private set; }
        public IOrderHeaderRepositary OrderHeader { get; private set; }
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Category = new CategoryRepositary(_db);
            Product = new ProductRepositary(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUser = new ApplicationUserRepositary(_db);
            OrderHeader = new OrderHeaderRepositary(_db);
            OrderDetail = new OrderDetailRepositary(_db);

        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
