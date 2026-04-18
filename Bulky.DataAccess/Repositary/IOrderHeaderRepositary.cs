using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public interface IOrderHeaderRepositary : IRepositary<OrderHeader>
    {
        void Update(OrderHeader obj);
    }
}
