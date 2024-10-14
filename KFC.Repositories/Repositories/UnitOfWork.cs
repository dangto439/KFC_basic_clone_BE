using KFC.Contract.Repositories.Interface;
using KFC.Repositories.Base;

namespace KFC.Repositories.Repositories
{
    public class UnitOfWork(KFCDBContext dbContext) : IUnitOfWork
    {
        private bool disposed = false;
        private readonly KFCDBContext _dbContext = dbContext;

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            //_dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            //_dbContext.Database.CommitTransaction();
        }

        public void RollBack()
        {
            //_dbContext.Database.RollbackTransaction();
        }
    }
}
