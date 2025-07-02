using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TradingProject.Application.Abstracts.Repositories;
using TradingProject.Domain.Entities;
using TradingProject.Persistence.Contexts;

namespace TradingProject.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly TradingDbCotext _context;

        private readonly DbSet<T> Table;

        public Repository(TradingDbCotext context)
        {
            _context = context;
            Table = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
                return;

            foreach (var entity in entities)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _context.Set<T>().Attach(entity);
                }
            }

            _context.Set<T>().RemoveRange(entities);

        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public IQueryable<T> GetByFiltered(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>>[]? include = null,
            bool isTracking = false)
        {
            IQueryable<T> query = Table;
            if (predicate is not null)
                query = query.Where(predicate);

            if (include is not null)
            {
                foreach (var includeExpression in include)
                    query = query.Include(includeExpression);
            }

            if (!isTracking)
                return query.AsNoTracking();

            return query;

        }

        public IQueryable<T> GetAll(bool isTracking = false)
        {
            if (!isTracking)
                return Table.AsNoTracking();
            return Table;
        }


        public IQueryable<T> GetAllFiltered(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>>[]? include = null,
            Expression<Func<T, object>>? orderby = null,
            bool isOrderByAsc = true,
            bool isTracking = false)
        {
            IQueryable<T> query = Table;
            if (predicate is not null)
                query = query.Where(predicate);

            if (include is not null)
            {
                foreach (var includeExpression in include)
                    query = query.Include(includeExpression);
            }

            if (orderby is not null)
            {
                if (isOrderByAsc)
                    query = query.OrderBy(orderby);
                else
                    query = query.OrderByDescending(orderby);
            }

            if (!isTracking)
                return query.AsNoTracking();

            return query;
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
