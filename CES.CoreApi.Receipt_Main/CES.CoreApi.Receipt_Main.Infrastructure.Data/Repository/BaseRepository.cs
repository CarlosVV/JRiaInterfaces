using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository
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
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbv)
            {
                var exception = HandleDbEntityValidationException(dbv);
                Trace.Write(exception);
                throw;
            }
            catch (DbUpdateException dbu)
            {
                var exception = HandleDbUpdateException(dbu);
                Trace.Write(exception);
                throw;
            }   
        }

        private Exception HandleDbEntityValidationException(DbEntityValidationException dbv)
        {
            var builder = new StringBuilder("A DbEntityValidationException was caught while saving changes. ");
            try
            {
                foreach (var eve in dbv.EntityValidationErrors)
                {
                    builder.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        builder.AppendFormat("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbEntityValidationException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbv);
        }

        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");
            var sex = dbu.InnerException as SqlException;
            
            try
            {
                if(sex != null)
                {
                    builder.Append($"{sex.Message}");
                    foreach (SqlError err in sex.Errors)
                    {
                        switch (err.Number)
                        {
                            case 208:
                                break;
                            default:
                                break;
                        }
                    }
                }

                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }

        public void Dispose()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
