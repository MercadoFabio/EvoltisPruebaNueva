using Service.Dtos.Common;
using Service.Dtos;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
        Task<OperationResponse> GetCategoriesAsync();
        Task<OperationResponsePaginated> GetCategoriesByConditionAsync(PaginacionRequest request);
        Task<OperationResponse> GetCategoryByIdAsync(int id);
        Task<OperationResponse> AddCategoryAsync(CategoryDto category);
        Task<OperationResponse> UpdateCategoryAsync(CategoryDto category);
        Task<OperationResponse> DeleteCategoryAsync(int id);
    }
}
