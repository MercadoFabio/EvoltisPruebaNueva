using System.Linq.Expressions;

namespace Repository.Interfaces
{

    public interface IRepository<TEntity, TDto>
    {
        Task<List<TEntity>> FindByConditionAsyncWithPagination(Expression<Func<TEntity, bool>> expression, int? pageSize, int? page);
        Task<List<TEntity>> FindAllAsync();
        Task<List<TEntity>> FindByConditionAsync(Func<TEntity, bool> expression);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
    }


}
