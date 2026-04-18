using Bulky.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public interface IUnitOfWork
    {
        ICategoryRepositary Category { get; }
        IProductRepositary Product { get; }
        ICompanyRepositary Company { get; }
        IShoppingCartRepositary ShoppingCart { get; }
        IApplicationUserRepositary ApplicationUser { get; }
        IOrderHeaderRepositary OrderHeader { get; }

        IOrderDetailRepositary OrderDetail { get; }

        void Save();
    }
}
