using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public class OrderDetailRepositary : Repositary<OrderDetail>, IOrderDetailRepositary
    {
        private ApplicationDbContext _db;
        public OrderDetailRepositary(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
