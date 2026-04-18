using Bulky.DataAccess.Repositary;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository
{
    public interface ICompanyRepositary : IRepositary<Company>
    {
        void Update(Company obj);
    }
}
