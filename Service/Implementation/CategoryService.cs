using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Domain.Repository.Implementation;
using Domain.Repository.Interfaces;
using Service.Dtos;
using Service.Dtos.Common;
using Service.Interfaces;
using Service.Validators;

namespace Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryValidator _validationRules;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, CategoryValidator validationRules, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _validationRules = validationRules;
            _mapper = mapper;
        }

        public async Task<OperationResponse> AddCategoryAsync(CategoryDto category)
        {
            var validateResult = await _validationRules.ValidateAsync(category);
            if (validateResult.IsValid)
            {
                Category newCategory = new()
                {
                    CreatedAt = DateTime.Now,
                    Name = category.Name,
                    Description = category.Description,
                };
                TCategory entity = await _categoryRepository.AddAsync(newCategory);

                return new OperationResponse(_mapper.Map(entity, category));
            }
            else
            {
                return new OperationResponse(validateResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }

        public async Task<OperationResponse> DeleteCategoryAsync(int id)
        {
            Category category = (await _categoryRepository.FindByConditionAsync(x => x.Id == id && !x.DeletedAt.HasValue)).FirstOrDefault();

            if (category != null)
            {
                category.DeletedAt = DateTime.Now;
                TCategory entity = await _categoryRepository.UpdateAsync(category);
                return new OperationResponse(_mapper.Map<CategoryDto>(category));
            }
            else
            {
                return new OperationResponse("No se pudo eliminar la categoría.", 400);
            }
        }

        public async Task<OperationResponse> GetCategoriesAsync()
        {
            List<Category> categories = await _categoryRepository.FindAllAsync();
            List<CategoryDto> categoriesDtos = _mapper.Map<List<CategoryDto>>(categories.Where(c => !c.DeletedAt.HasValue).ToList());
            if (categoriesDtos.Count > 0)
            {
                return new OperationResponse(categoriesDtos);
            }
            else
            {
                return new OperationResponse("No se encontraron categorías.", 404);
            }
        }

        public async Task<OperationResponsePaginated> GetCategoriesByConditionAsync(PaginacionRequest request)
        {
            (List<Category> paginatedList, int totalItems) query = await _categoryRepository.FindByConditionAsyncWithPagination(x =>
                                       (!x.DeletedAt.HasValue
                                       && (string.IsNullOrEmpty(request.Name) || x.Name.ToUpper().Contains(request.Name.ToUpper()))
                                       && (request.IdCategory == 0 || x.Id == request.IdCategory)),
                                   request.PageSize,
                                   request.Page
                               );

            List<CategoryDto> categoriesDtos = _mapper.Map<List<CategoryDto>>(query.paginatedList);
            if (categoriesDtos.Count > 0)
            {
                return new OperationResponsePaginated(categoriesDtos, query.totalItems);
            }
            else
            {
                return new OperationResponsePaginated();
            }
        }

        public async Task<OperationResponse> GetCategoryByIdAsync(int id)
        {
            Category category = (await _categoryRepository.FindByConditionAsync(x => x.Id == id && !x.DeletedAt.HasValue)).FirstOrDefault();
            if (category != null)
            {
                return new OperationResponse(_mapper.Map<CategoryDto>(category));
            }
            else
            {
                return new OperationResponse("No se encontró la categoría.", 400);
            }
        }

        public async Task<OperationResponse> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var validateResult = await _validationRules.ValidateAsync(categoryDto);

            Category category = (await _categoryRepository.FindByConditionAsync(x => !x.DeletedAt.HasValue &&  x.Id == categoryDto.Id)).FirstOrDefault();

            if (validateResult.IsValid && category != null)
            {
                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;
                category.UpdatedAt = DateTime.Now;

                TCategory entity = await _categoryRepository.UpdateAsync(category);

                return new OperationResponse(_mapper.Map(entity, categoryDto));
            }
            else
            {
                return new OperationResponse(validateResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }
    }
}
