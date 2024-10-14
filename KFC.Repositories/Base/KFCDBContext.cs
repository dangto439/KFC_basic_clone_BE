using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KFC.Entity;

namespace KFC.Repositories.Base
    {
    public class KFCDBContext
    {
        public object Database { get; internal set; }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }

        //public KFCDBContext(DbContextOptions<KFCDBContext> options) : base(options) { }

        // user
        //public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }

        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
