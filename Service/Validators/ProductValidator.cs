using Domain.Dtos;
using FluentValidation;
using Service.Dtos;

namespace Domain.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("El nombre del producto es obligatorio");
            RuleFor(product => product.Price).NotNull().GreaterThan(0).WithMessage("El precio debe ser mayor que cero");
            RuleFor(product => product.Quantity).NotNull().GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero");
            RuleFor(product => product.IdCategory).NotNull().GreaterThan(0).WithMessage("La categoría es obligatoria");
        }
    }
}
