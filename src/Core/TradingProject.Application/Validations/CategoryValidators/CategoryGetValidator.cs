using FluentValidation;
using TradingProject.Application.DTOs.CategoryDto;

namespace TradingProject.Application.Validations.CategoryValidators
{
    public class CategoryGetValidator : AbstractValidator<CategoryGetDto>
    {
        public CategoryGetValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name con not be empty");
        }
    }

}
