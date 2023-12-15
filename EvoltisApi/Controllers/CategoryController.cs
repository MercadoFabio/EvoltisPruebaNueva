using Microsoft.AspNetCore.Mvc;
using Service.Dtos.Common;
using Service.Dtos;
using Service.Interfaces;

namespace EvoltisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPut]
        public async Task<IActionResult> AddOrUpdateCategoryAsync(CategoryDto category)
        {

            try
            {
                var response = new OperationResponse();
                if (category.Id == 0)
                {
                    response = await _categoryService.AddCategoryAsync(category);
                }
                else
                {
                    response = await _categoryService.UpdateCategoryAsync(category);
                }
                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            try
            {

                var response = await _categoryService.GetCategoriesAsync();
                return response.Success ? Ok(response) : BadRequest(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetCategoriesAsync([FromBody] PaginacionRequest request)
        {
            try
            {

                var response = await _categoryService.GetCategoriesByConditionAsync(request);
                return response.Success ? Ok(response) : BadRequest(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int id)
        {
            try
            {
                var response = await _categoryService.GetCategoryByIdAsync(id);
                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                var response = await _categoryService.DeleteCategoryAsync(id);
                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }

    }
}
