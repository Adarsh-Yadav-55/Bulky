using Bulky.DataAccess.Repository;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repositary
{
    public interface IShoppingCartRepositary : IRepositary<ShoppingCart>
    {
        void Update(ShoppingCart obj);
    }
}
