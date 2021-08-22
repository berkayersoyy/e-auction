using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EAuction.Core.Repositories.Abstractions;
using EAuction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EAuction.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly WebAppContext _context;

        public Repository(WebAppContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeStrings = null,
            bool disableTracking = true)
        {
            IQueryable<T> queryable = _context.Set<T>();
            if (!string.IsNullOrWhiteSpace(includeStrings))
            {
                queryable = queryable.Include(includeStrings);
            }

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(queryable).ToListAsync();
            }

            return await queryable.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> queryable = _context.Set<T>();
            if (disableTracking)
            {
                queryable = queryable.AsNoTracking();
            }

            if (includes != null)
            {
                queryable = includes.Aggregate(queryable, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(queryable).ToListAsync();
            }

            return await queryable.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}