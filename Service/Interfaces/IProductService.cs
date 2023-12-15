using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Dtos.Common;
using System.Linq.Expressions;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<OperationResponse> GetProductsAsync();
        Task<OperationResponsePaginated> GetProductsByConditionAsync(PaginacionRequest request);
        Task<OperationResponse> GetProductByIdAsync(int id);
        Task<OperationResponse> AddProductAsync(ProductDto product);
        Task<OperationResponse> UpdateProductAsync(ProductDto product);
        Task<OperationResponse> DeleteProductAsync(int id);
    }
}
