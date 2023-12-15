using System.Linq.Expressions;

namespace Domain.Repository.Interfaces
{

    public interface IRepository<TEntity, TDto>
    {
        Task<(List<TDto>,int)> FindByConditionAsyncWithPagination(Func<TEntity, bool> expression, int? pageSize, int? page);
        Task<List<TDto>> FindAllAsync();
        Task<List<TDto>> FindByConditionAsync(Func<TEntity, bool> expression);
        Task<TEntity> AddAsync(TDto entity);
        Task<TEntity> UpdateAsync(TDto entity);
        Task<TEntity> DeleteAsync(TDto entity);
        List<TDto> FindByCondition(Func<TEntity, bool> expression);

    }


}
