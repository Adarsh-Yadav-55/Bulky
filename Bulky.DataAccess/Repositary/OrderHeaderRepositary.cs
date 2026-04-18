using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public class OrderHeaderRepositary : Repositary<OrderHeader>, IOrderHeaderRepositary
    {
        private ApplicationDbContext _db;
        public OrderHeaderRepositary(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }
    }
}
