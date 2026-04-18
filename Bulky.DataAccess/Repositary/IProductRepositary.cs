using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public interface IProductRepositary : IRepositary<Product>
    {
        void Update(Product product);
        void Save();

    }
}
