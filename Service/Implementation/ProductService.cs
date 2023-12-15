using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Domain.Repository.Interfaces;
using Domain.Validators;
using Service.Dtos;
using Service.Dtos.Common;
using Service.Interfaces;

namespace Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductValidator _validationRules;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ProductValidator validationRules, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _validationRules = validationRules;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResponse> AddProductAsync(ProductDto product)
        {

            var validateResult = await _validationRules.ValidateAsync(product);
            if (validateResult.IsValid)
            {
                Product newProduct = new()
                {
                    CreatedAt = DateTime.Now,
                    Name = product.Name,
                    Price = product.Price,
                    IdCategory = product.IdCategory,
                    Quantity = product.Quantity
                };
                TProduct entity = await _productRepository.AddAsync(newProduct);

                return new OperationResponse(_mapper.Map(entity, product));
            }
            else
            {
                return new OperationResponse(validateResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }

        public async Task<OperationResponse> DeleteProductAsync(int id)
        {

            Product product = (await _productRepository.FindByConditionAsync(x => x.Id == id && !x.DeletedAt.HasValue)).FirstOrDefault();

            if (product != null)
            {
                product.DeletedAt = DateTime.Now;
                TProduct entity = await _productRepository.UpdateAsync(product);
                return new OperationResponse(_mapper.Map<ProductDto>(product));
            }
            else
            {
                return new OperationResponse("No se pudo eliminar el producto.", 400);
            }
        }

        public async Task<OperationResponse> GetProductByIdAsync(int id)
        {
            Product product = (await _productRepository.FindByConditionAsync(x => x.Id == id && !x.DeletedAt.HasValue)).FirstOrDefault();
            if (product != null)
            {
                return new OperationResponse(_mapper.Map<ProductDto>(product));
            }
            else
            {
                return new OperationResponse("No se encontró el producto.", 404);
            }
        }

        public async Task<OperationResponse> GetProductsAsync()
        {
            List<Product> listProduct = await _productRepository.FindAllAsync();
            List<ProductDto> listProductDto = _mapper.Map<List<ProductDto>>(listProduct.Where(x => !x.DeletedAt.HasValue));
            if (listProductDto.Count > 0)
            {
                listProductDto = listProductDto.Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    IdCategory = x.IdCategory,
                    CategoryName = _categoryRepository.FindByCondition(c => c.Id == x.IdCategory).Select(x => x.Name).FirstOrDefault()
                }).ToList();
                return new OperationResponse(listProductDto);
            }
            else
            {
                return new OperationResponse("No se encontraron productos.", 404);

            }
        }

        public async Task<OperationResponsePaginated> GetProductsByConditionAsync(PaginacionRequest request)
        {
            (List<Product> paginatedList, int totalItems) query = await _productRepository.FindByConditionAsyncWithPagination(
                                    x =>
                                        (!x.DeletedAt.HasValue
                                        && (string.IsNullOrEmpty(request.Name) || x.Name.ToUpper().Contains(request.Name.ToUpper()))
                                        && (request.IdCategory == 0 || x.IdCategory == request.IdCategory)),
                                    request.PageSize,
                                    request.Page
                                );

            List<ProductDto> listProductDto = _mapper.Map<List<ProductDto>>(query.paginatedList);
            if (listProductDto.Count > 0)
            {
                listProductDto = listProductDto.Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    IdCategory = x.IdCategory,
                    CategoryName = _categoryRepository.FindByCondition(c => c.Id == x.IdCategory).Select(x => x.Name).FirstOrDefault()
                }).ToList();

                return new OperationResponsePaginated(listProductDto, query.totalItems);
            }
            else
            {
                return new OperationResponsePaginated();
            }
        }

        public async Task<OperationResponse> UpdateProductAsync(ProductDto productDto)
        {
            var validateResult = await _validationRules.ValidateAsync(productDto);

            Product product = (await _productRepository.FindByConditionAsync(x => !x.DeletedAt.HasValue && x.Id == productDto.Id)).FirstOrDefault();

            if (validateResult.IsValid && product != null)
            {
                product.Name = productDto.Name;
                product.Price = productDto.Price;
                product.Quantity = productDto.Quantity;
                product.IdCategory = productDto.IdCategory;
                product.UpdatedAt = DateTime.Now;

                TProduct entity = await _productRepository.UpdateAsync(product);

                return new OperationResponse(_mapper.Map(entity, productDto));
            }
            else
            {
                return new OperationResponse(validateResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }
    }
}
