using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public virtual T Get(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null)
        {
            if (includes == null) //There are no tables to join
            {
                if (asNoTracking) // read only copy for display purposes
                {
                    return _dbContext.Set<T>().AsNoTracking().Where(predicate).FirstOrDefault();
                }
                else // it needs to be tracked
                {
                    return _dbContext.Set<T>().Where(predicate).FirstOrDefault();
                }
            }
            else //this has includes other objects or tables
            {
                IQueryable<T> queryable = _dbContext.Set<T>();
                foreach (string includesProperty in includes.Split(new char[]
                { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includesProperty);
                }
                if (asNoTracking)
                {
                    return queryable.AsNoTracking().Where(predicate).FirstOrDefault();
                }
                else
                {
                    return queryable.Where(predicate).FirstOrDefault();
                }
            }

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (query == null)
            {
                throw new InvalidOperationException("Query is null.");
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            // include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null)
        {
            if (includes == null) //There are no tables to join
            {
                if (asNoTracking) // read only copy for display purposes
                {
                    return await _dbContext.Set<T>().AsNoTracking().Where(predicate).FirstOrDefaultAsync();
                }
                else // it needs to be tracked
                {
                    return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
                }
            }
            else //this has includes other objects or tables
            {
                IQueryable<T> queryable = _dbContext.Set<T>();
                foreach (string includesProperty in includes.Split(new char[]
                { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(includesProperty);
                }
                if (asNoTracking)
                {
                    return await queryable.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
                }
                else
                {
                    return await queryable.Where(predicate).FirstOrDefaultAsync();
                }
            }
        }

        public virtual T GetByID(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> List()
        {
            return _dbContext.Set<T>().ToList().AsEnumerable();
        }

        public virtual IEnumerable<T> List(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null)
        {
            IQueryable<T> queriable = _dbContext.Set<T>();
            if (predicate != null && includes == null) // does have a where, but does not include others
            {
                return _dbContext.Set<T>().Where(predicate).AsEnumerable();
            }
            else if (includes != null)
            {
                foreach (string includesProperty in includes.Split(new char[]
                    {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    queriable = queriable.Include(includesProperty);
                }
            }
            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return queriable.AsEnumerable();
                }
                else
                {
                    return queriable.OrderBy(orderBy).ToList().AsEnumerable();
                }
            }
            else
            {
                if (orderBy == null)
                {
                    return queriable.Where(predicate).ToList().AsEnumerable();
                }
                else
                {
                    return queriable.Where(predicate).OrderBy(orderBy).ToList().AsEnumerable();
                }
            }
        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null)
        {
            IQueryable<T> queriable = _dbContext.Set<T>();
            if (predicate != null && includes == null) // does have a where, but does not include others
            {
                return await _dbContext.Set<T>().Where(predicate).ToListAsync();
            }
            else if (includes != null)
            {
                foreach (string includesProperty in includes.Split(new char[]
                    {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    queriable = queriable.Include(includesProperty);
                }
            }
            if (predicate == null)
            {
                if (orderBy == null)
                {
                    return await queriable.ToListAsync();
                }
                else
                {
                    return await queriable.OrderBy(orderBy).ToListAsync();
                }
            }
            else
            {
                if (orderBy == null)
                {
                    return await queriable.Where(predicate).ToListAsync();
                }
                else
                {
                    return await queriable.Where(predicate).OrderBy(orderBy).ToListAsync(); ;
                }
            }
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}


