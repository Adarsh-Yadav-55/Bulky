using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repositary
{
    public class ApplicationUserRepositary : Repositary<ApplicationUser>, IApplicationUserRepositary
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepositary(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //public void Update(ApplicationUser obj)
        //{
        //    _db.ApplicationUsers.Update(obj);
        //}
    }
}