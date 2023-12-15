using Domain.Dtos;
using FluentValidation;
using Service.Dtos;

namespace Service.Validators
{
    public class CategoryValidator: AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name).NotEmpty().WithMessage("El nombre de la categoría es obligatorio");
        }
    }
}
