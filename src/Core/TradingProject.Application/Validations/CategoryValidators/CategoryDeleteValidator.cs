using FluentValidation;
using TradingProject.Application.DTOs.CategoryDtos;

namespace TradingProject.Application.Validations.CategoryValidators
{
    public class CategoryDeleteDtoValidator : AbstractValidator<CategoryDeleteDto>
    {
        public CategoryDeleteDtoValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Id can't be empty");
        }
    }
}
