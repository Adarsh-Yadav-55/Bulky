using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repositary;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository
{
    public class CompanyRepository : Repositary<Company>, ICompanyRepositary
    {
        private ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}