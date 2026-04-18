using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky.DataAccess.Repositary
{
    public interface ICategoryRepositary:IRepositary<Category>
    {
        void Update(Category category);
        void Save();

    }
}
