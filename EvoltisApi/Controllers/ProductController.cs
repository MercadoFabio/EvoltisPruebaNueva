using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Dtos.Common;
using Service.Interfaces;

namespace EvoltisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPut]
        public async Task<IActionResult> AddOrUpdateProductAsync(ProductDto product)
        {

            try
            {
                var response = new OperationResponse();
                if (product.Id == 0)
                {
                    response = await _productService.AddProductAsync(product);
                }
                else
                {
                    response = await _productService.UpdateProductAsync(product);
                }
                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {

                var response = await _productService.GetProductsAsync();
                return response.Success ? Ok(response) : BadRequest(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetProductsAsync([FromBody] PaginacionRequest request)
        {
            try
            {

                var response = await _productService.GetProductsByConditionAsync(request);
                return response.Success ? Ok(response) : BadRequest(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(id);
                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(id);
                return response.Success ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OperationResponse(500));
            }
        }

    }
}
