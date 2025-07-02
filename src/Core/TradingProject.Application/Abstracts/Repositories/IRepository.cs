using System.Linq.Expressions;
using TradingProject.Domain.Entities;

namespace TradingProject.Application.Abstracts.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        //Read IRepository
        Task<T?> GetByIdAsync(Guid id);
        IQueryable<T> GetAll(bool isTracking = false);


        IQueryable<T> GetByFiltered(Expression<Func<T, bool>> predicate,
                             Expression<Func<T, object>>[]? include = null,
                             bool isTracking = false);
        IQueryable<T> GetAllFiltered(Expression<Func<T, bool>> predicate,
                             Expression<Func<T, object>>[]? include = null,
                             Expression<Func<T, object>>? orderby = null,
                             bool isOrderByAsc = true,
                             bool isTracking = false);
        //Write IRepository
        Task AddAsync(T entity);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);

        Task SaveChangeAsync();
    }
}
