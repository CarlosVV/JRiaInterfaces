using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class BaseRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return GetWhere(predicate).FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return GetWhere(null);
        }
        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate = null)
        {
            IEnumerable<T> result = _dbContext.Set<T>().AsEnumerable();
            return (predicate == null) ? result : result.AsQueryable().Where<T>(predicate);
        }

        public void Add(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = GetAll(predicate);
            foreach (T entity in entities)
                _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async void Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
